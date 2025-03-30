using System;
using System.Linq;
using System.Threading.Tasks;
using SistemaVenta.DAL.dbcontext;
using SistemaVenta.Model;
using Microsoft.EntityFrameworkCore;
using SistemaVenta.DAL.Repositorios.Contrato;
using System.Collections.Generic;

namespace SistemaVenta.DAL.Repositorios
{
    public class VentaRepository : GenericRepository<Venta>, IVentaRepository
    {
        private readonly DbventaContext _dbcontext;

        public VentaRepository(DbventaContext dbcontext) : base(dbcontext)
        {
            _dbcontext = dbcontext;
        }

        // Obtener venta por ID con detalles y producto relacionado
        public async Task<Venta> ObtenerPorId(int id)
        {
            return await _dbcontext.Venta
                .Include(v => v.DetalleVenta)
                .ThenInclude(dv => dv.IdProductoNavigation)
                .FirstOrDefaultAsync(v => v.IdVenta == id);
        }

    



        // Eliminar una venta y actualizar el stock de productos
        public async Task<bool> Eliminar(int idVenta)
        {
            var venta = await ObtenerPorId(idVenta);
            if (venta == null)
                throw new KeyNotFoundException($"Venta con ID {idVenta} no encontrada.");

            using var transaction = await _dbcontext.Database.BeginTransactionAsync();
            try
            {
                // Actualiza el stock de los productos en un solo ciclo
                foreach (var detalle in venta.DetalleVenta)
                {
                    var producto = await _dbcontext.Productos.FindAsync(detalle.IdProducto);
                    if (producto == null)
                        throw new KeyNotFoundException($"Producto con ID {detalle.IdProducto} no encontrado.");

                    producto.Stock += detalle.Cantidad;
                }

                _dbcontext.Venta.Remove(venta);  // Elimina la venta
                await _dbcontext.SaveChangesAsync();  // Guarda los cambios

                await transaction.CommitAsync();  // Confirma la transacción
                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();  // Revierte si algo sale mal
                throw new InvalidOperationException("Error al eliminar la venta.", ex);
            }
        }

        // Registrar una nueva venta y descontar stock
        public async Task<Venta> Registrar(Venta modelo)
        {
            using var transaction = await _dbcontext.Database.BeginTransactionAsync();
            try
            {
                // Verifica y actualiza el stock de los productos antes de registrar la venta
                foreach (var detalle in modelo.DetalleVenta)
                {
                    var producto = await _dbcontext.Productos.FindAsync(detalle.IdProducto);
                    if (producto == null)
                        throw new KeyNotFoundException($"Producto con ID {detalle.IdProducto} no encontrado.");

                    if (producto.Stock < detalle.Cantidad)
                        throw new InvalidOperationException($"Stock insuficiente para el producto con ID {detalle.IdProducto}.");

                    producto.Stock -= detalle.Cantidad;  // Descontar del stock
                }

                // Actualizar correlativo de documento
                var correlativo = await _dbcontext.NumeroDocumentos.FirstOrDefaultAsync()
                                  ?? throw new InvalidOperationException("Correlativo no encontrado.");

                correlativo.UltimoNumero++;
                modelo.NumeroDocumento = correlativo.UltimoNumero.ToString("D4");

                await _dbcontext.Venta.AddAsync(modelo);  // Agregar la nueva venta
                await _dbcontext.SaveChangesAsync();  // Guardar cambios

                await transaction.CommitAsync();  // Confirmar la transacción
                return modelo;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();  // Revierte si ocurre un error
                throw new InvalidOperationException("Error durante el registro de la venta.", ex);
            }
        }
    }
}

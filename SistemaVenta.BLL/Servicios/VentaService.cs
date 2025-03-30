using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DAL.dbcontext;
using SistemaVenta.DAL.Repositorios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.Model;

namespace SistemaVenta.BLL.Servicios
{
    public class VentaService : IVentaService
    {
        private readonly IVentaRepository _ventaRepositorio;
        private readonly IGenericRepository<DetalleVenta> _detalleVentaRepositorio;
        private readonly IMapper _mapper;
        private readonly DbventaContext _dbcontext;
        private readonly IProductoService _productoRepositorio;

        public VentaService(
            IVentaRepository ventaRepositorio,
            IGenericRepository<DetalleVenta> detalleVentaRepositorio,
            IMapper mapper,
            DbventaContext dbcontext,
            IProductoService productoRepositorio)
        {
            _ventaRepositorio = ventaRepositorio;
            _detalleVentaRepositorio = detalleVentaRepositorio;
            _dbcontext = dbcontext;
            _mapper = mapper;
            _productoRepositorio = productoRepositorio;
        }

        public async Task<VentaDTO> Registrar(VentaDTO modelo)
        {
            try
            {
                var venta = _mapper.Map<Venta>(modelo);
                var ventaGenerada = await _ventaRepositorio.Registrar(venta);

                if (ventaGenerada.IdVenta == 0)
                    throw new InvalidOperationException("No se pudo crear la venta.");

                return _mapper.Map<VentaDTO>(ventaGenerada);
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Error en la base de datos durante el registro de la venta.", ex);
            }
        }

        public async Task<List<VentaDTO>> Historial(string buscarPor, string numeroVenta, string fechaInicio, string fechaFin, string dia)
        {
            try
            {
                IQueryable<Venta> query = await _ventaRepositorio.Consultar();
                DateTime? fechInicio = null, fechFin = null, fechaDia = null;

                if (buscarPor == "fecha")
                {
                    fechInicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-PE"));
                    fechFin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-PE"));
                    query = query.Where(v => v.FechaRegistro.Value.Date >= fechInicio.Value && v.FechaRegistro.Value.Date <= fechFin.Value);
                }
                else if (buscarPor == "Dia")
                {
                    fechaDia = DateTime.ParseExact(dia, "dd/MM/yyyy", new CultureInfo("es-PE"));
                    query = query.Where(v => v.FechaRegistro.Value.Date == fechaDia.Value.Date);
                }
                else
                {
                    query = query.Where(v => v.NumeroDocumento == numeroVenta);
                }

                var ventas = await query.Include(v => v.DetalleVenta).ThenInclude(dv => dv.IdProductoNavigation).ToListAsync();
                return _mapper.Map<List<VentaDTO>>(ventas);
            }
            catch (FormatException ex)
            {
                throw new ArgumentException("Formato de fecha inválido.", ex);
            }
        }

        public async Task<List<ReporteDTO>> Reporte(string fechaInicio, string fechaFin, string producto, string tipoPago)
        {
            try
            {
                var fechInicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-PE"));
                var fechFin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-PE"));

                var query = await _detalleVentaRepositorio.Consultar();
                query = query.Include(dv => dv.IdProductoNavigation)
                             .Include(dv => dv.IdVentaNavigation)
                             .Where(dv => dv.IdVentaNavigation.FechaRegistro >= fechInicio && dv.IdVentaNavigation.FechaRegistro <= fechFin);

                if (!string.IsNullOrEmpty(producto))
                    query = query.Where(dv => dv.IdProductoNavigation.Nombre.Contains(producto));

                if (!string.IsNullOrEmpty(tipoPago))
                    query = query.Where(dv => dv.IdVentaNavigation.TipoPago.Contains(tipoPago));

                var detalles = await query.ToListAsync();
                return _mapper.Map<List<ReporteDTO>>(detalles);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al generar el reporte.", ex);
            }
        }

        public async Task Eliminar(int id)
        {
            try
            {
                var venta = await _ventaRepositorio.ObtenerPorId(id);
                if (venta == null)
                    throw new KeyNotFoundException($"La venta con ID {id} no fue encontrada.");

                // Itera sobre los detalles de la venta
                foreach (var detalle in venta.DetalleVenta)
                {
                    // Verifica si el IdProducto es nulo antes de intentar obtener el producto
                    if (detalle.IdProducto == null)
                    {
                        throw new InvalidOperationException($"El detalle de venta no tiene un producto asociado para el ID {detalle.IdDetalleVenta}.");
                    }
                    await _productoRepositorio.EditarStock((int)detalle.IdProducto, (int)detalle.Cantidad);
                }

                // Elimina los detalles de la venta
                await _detalleVentaRepositorio.EliminarRango(venta.DetalleVenta.ToList());

                // Elimina la venta
                if (!await _ventaRepositorio.Eliminar(venta))
                    throw new InvalidOperationException($"No se pudo eliminar la venta con ID {id}");

                await _dbcontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al eliminar la venta.", ex);
            }
        }


    }
}

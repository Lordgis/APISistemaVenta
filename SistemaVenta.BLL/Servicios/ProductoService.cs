using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DAL.Repositorios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.DAL.dbcontext;
using SistemaVenta.Model;

namespace SistemaVenta.BLL.Servicios
{
    public class ProductoService : IProductoService
    {
        private readonly IGenericRepository<Producto> _productoRepositorio;
        private readonly IMapper _mapper;
        private readonly DbventaContext _dbcontext;
        private readonly IVentaRepository _ventaRepository;

        public ProductoService(IGenericRepository<Producto> productoRepositorio, IMapper mapper, DbventaContext dbcontext, IVentaRepository ventaRepository)
        {
            _productoRepositorio = productoRepositorio;
            _mapper = mapper;
            _dbcontext = dbcontext;
            _ventaRepository = ventaRepository;
        }


        public async Task<List<ProductoDTO>> Lista()
        {
            try
            {

                var queryProducto = await _productoRepositorio.Consultar();

                var listaProductos = queryProducto.Include(cat => cat.IdCategoriaNavigation).ToList();
                
                listaProductos = queryProducto.Include(cat => cat.IdProveedorNavigation).ToList();

                return _mapper.Map<List<ProductoDTO>>(listaProductos.ToList());

            } catch {
                throw;
            }
        }
        public async Task<ProductoDTO> Crear(ProductoDTO modelo)
        {
            try
            {
                var productoCreado = await _productoRepositorio.Crear(_mapper.Map<Producto>(modelo));

                if (productoCreado.IdProducto == 0)
                    throw new TaskCanceledException("No se pudo crear");

                return _mapper.Map<ProductoDTO>(productoCreado);

            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Editar(ProductoDTO modelo)
        {
            try
            {

                var productoModelo = _mapper.Map<Producto>(modelo);
                var productoEncontrado = await _productoRepositorio.Obtener(u =>
                    u.IdProducto == productoModelo.IdProducto
                );

                if(productoEncontrado == null)
                    throw new TaskCanceledException("El producto no existe");


                productoEncontrado.Nombre = productoModelo.Nombre;
                productoEncontrado.IdCategoria = productoModelo.IdCategoria;
                productoEncontrado.Stock = productoModelo.Stock;
                productoEncontrado.Precio = productoModelo.Precio;
                productoEncontrado.PrecioVenta = productoModelo.PrecioVenta;
                productoEncontrado.IdProveedor = productoModelo.IdProveedor;
                productoEncontrado.EsActivo = productoModelo.EsActivo;

                bool respuesta = await _productoRepositorio.Editar(productoEncontrado);

                if(!respuesta)
                    throw new TaskCanceledException("No se pudo editar"); ;


                return respuesta;

            }
            catch
            {
                throw;
            }
        }  
        public async Task<bool> Eliminar(int id)
        {
            try
            {

                var productoEncontrado = await _productoRepositorio.Obtener(p => p.IdProducto == id);

                if(productoEncontrado == null)
                    throw new TaskCanceledException("El producto no existe");

                bool respuesta = await _productoRepositorio.Eliminar(productoEncontrado);


                if (!respuesta)
                    throw new TaskCanceledException("No se pudo elminar"); ;

                return respuesta;

            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> EditarStock(int idProducto, int nuevoStock)
        {
            try
            {
                if (nuevoStock < 0)
                    throw new ArgumentException("El stock no puede ser negativo.");
                var producto = await _productoRepositorio.Obtener(p => p.IdProducto == idProducto);
                if (producto == null)
                    throw new KeyNotFoundException($"El producto con ID {idProducto} no existe.");
                producto.Stock += nuevoStock;
                bool respuesta = await _productoRepositorio.Editar(producto);

                if (!respuesta)
                    throw new InvalidOperationException("No se pudo actualizar el stock del producto.");

                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el stock del producto.", ex);
            }
        }


        public async Task<Producto> ObtenerProductoPorId(int id)
        {
            var idventa = _ventaRepository.ObtenerPorId(id);

            return await _dbcontext.Productos
                            .Include(P => P.IdProducto)
                            .FirstOrDefaultAsync(p => p.IdProducto == id);
        }
    }
}

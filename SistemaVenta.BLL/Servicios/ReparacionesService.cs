using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DAL.Repositorios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.DAL.dbcontext;
using SistemaVenta.BLL.Excepciones;
using SistemaVenta.Model;

namespace SistemaVenta.BLL.Servicios
{
    public class ReparacionesService : IReparacionService
    {
        private readonly IGenericRepository<Reparaciones> _reparacionesRepositorio;
        private readonly IMapper _mapper;

        public ReparacionesService(IGenericRepository<Reparaciones> reparacionesRepositorio, IMapper mapper)
        {
            _reparacionesRepositorio = reparacionesRepositorio;
            _mapper = mapper;
        }

        public async Task<List<ReparacionesDTO>> Lista()
        {
            try
            {
                var queryReparaciones = await _reparacionesRepositorio.Consultar();
                var listaReparaciones = await queryReparaciones.ToListAsync();
                return _mapper.Map<List<ReparacionesDTO>>(listaReparaciones);
            }
            catch (DbUpdateException ex)
            {
                // Manejar excepciones específicas de Entity Framework aquí
                throw new Exception("Error al consultar las reparaciones", ex);
            }
            catch (Exception ex)
            {
                // Manejar otras excepciones aquí
                throw new Exception("Error inesperado al consultar las reparaciones", ex);
            }
        }


        public async Task<ReparacionesDTO> Crear(ReparacionesDTO modelo)
        {
            try
            {
                var reparacionCreada = await _reparacionesRepositorio.Crear(_mapper.Map<Reparaciones>(modelo));
                if (reparacionCreada.IdReparacion == 0)
                    throw new InvalidOperationException("No se pudo crear la reparación");

                return _mapper.Map<ReparacionesDTO>(reparacionCreada);
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Error al crear la reparación", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al crear la reparación", ex);
            }
        }
        public async Task<bool> Editar(ReparacionesDTO modelo)
        {
            try
            {
                var reparacionModelo = _mapper.Map<Reparaciones>(modelo);
                var reparacionEncontrada = await _reparacionesRepositorio.Obtener(u => u.IdReparacion == reparacionModelo.IdReparacion);

                if (reparacionEncontrada == null)
                {
                    throw new ReparacionNotFoundException($"La reparación con ID {modelo.IdReparaciones} no existe");
                }

                reparacionEncontrada.Nombre = reparacionModelo.Nombre;
                reparacionEncontrada.IdCategoria = reparacionModelo.IdCategoria;
                reparacionEncontrada.Precio = reparacionModelo.Precio;

                bool respuesta = await _reparacionesRepositorio.Editar(reparacionEncontrada);

                if (!respuesta)
                {
                    throw new ReparacionUpdateException("No se pudo editar la reparación");
                }

                return respuesta;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ReparacionUpdateException("Error de concurrencia al editar la reparación", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new ReparacionUpdateException("Error al editar la reparación", ex);
            }
            catch (Exception ex)
            {
                throw new ReparacionUpdateException("Error inesperado al editar la reparación", ex);
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var reparacionEncontrada = await _reparacionesRepositorio.Obtener(p => p.IdReparacion == id);

                if (reparacionEncontrada == null)
                {
                    throw new ReparacionNotFoundException($"La reparación con ID {id} no existe");
                }

                bool respuesta = await _reparacionesRepositorio.Eliminar(reparacionEncontrada);

                if (!respuesta)
                {
                    throw new ReparacionDeleteException("No se pudo eliminar la reparación");
                }

                return respuesta;
            }
            catch (DbUpdateException ex)
            {
                throw new ReparacionDeleteException("Error al eliminar la reparación", ex);
            }
            catch (Exception ex)
            {
                throw new ReparacionDeleteException("Error inesperado al eliminar la reparación", ex);
            }
        }

    }
   }

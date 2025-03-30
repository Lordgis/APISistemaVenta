using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DAL.Repositorios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.DAL.dbcontext;
using Microsoft.EntityFrameworkCore;
using SistemaVenta.Model;

namespace SistemaVenta.BLL.Servicios
{
    public class ProveedorService : IProveedorService
    {
        private readonly IGenericRepository<Proveedor> _ProveedorRepositorio;
        private readonly IMapper _mapper;

        public ProveedorService(IGenericRepository<Proveedor> ProveedorRepositorio, IMapper mapper)
        {
            _ProveedorRepositorio = ProveedorRepositorio;
            _mapper = mapper;
        }

        public async Task<List<ProveedorDTO>> Lista()
        {
            try
            {

                var listaProveedor = await _ProveedorRepositorio.Consultar();
                return _mapper.Map<List<ProveedorDTO>>(listaProveedor.ToList());

            }
            catch
            {
                throw;
            }
        }

        public async Task<ProveedorDTO> Crear(ProveedorDTO modelo)
        {
            try
            {
                var ProveedorCreado = await _ProveedorRepositorio.Crear(_mapper.Map<Proveedor>(modelo));

                if (ProveedorCreado.IdProveedor == 0)
                    throw new TaskCanceledException("No se pudo crear");

                return _mapper.Map<ProveedorDTO>(ProveedorCreado);

            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Editar(ProveedorDTO modelo)
        {
            try
            {

                var ProveedorModelo = _mapper.Map<Proveedor>(modelo);
                var ProveedorEncontrado = await _ProveedorRepositorio.Obtener(u =>
                    u.IdProveedor == ProveedorModelo.IdProveedor
                    );

                if (ProveedorEncontrado == null)
                    throw new TaskCanceledException("El Proveedor no existe");


                ProveedorEncontrado.NombreEmpresa = ProveedorModelo.NombreEmpresa;
                ProveedorEncontrado.ContactoPersona = ProveedorModelo.ContactoPersona;
                ProveedorEncontrado.Telefono = ProveedorModelo.Telefono;
                ProveedorEncontrado.Correo = ProveedorModelo.Correo;
                ProveedorEncontrado.Direccion = ProveedorModelo.Direccion;
                bool respuesta = await _ProveedorRepositorio.Editar(ProveedorEncontrado);

                if (!respuesta)
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

                var ProveedorEncontrado = await _ProveedorRepositorio.Obtener(p => p.IdProveedor == id);

                if (ProveedorEncontrado == null)
                    throw new TaskCanceledException("El Proveedor no existe");

                bool respuesta = await _ProveedorRepositorio.Eliminar(ProveedorEncontrado);


                if (!respuesta)
                    throw new TaskCanceledException("No se pudo elminar"); ;

                return respuesta;

            }
            catch
            {
                throw;
            }
        }


    }
}

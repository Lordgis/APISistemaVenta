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
    public class ClienteService : IClienteService
    {
        private readonly IGenericRepository<Cliente> _ClienteRepositorio;
        private readonly IMapper _mapper;

        public ClienteService(IGenericRepository<Cliente> ClienteRepositorio, IMapper mapper)
        {
            _ClienteRepositorio = ClienteRepositorio;
            _mapper = mapper;
        }

        public async Task<List<ClienteDTO>> Lista()
        {
            try
            {

                var listaCliente = await _ClienteRepositorio.Consultar();
                return _mapper.Map<List<ClienteDTO>>(listaCliente.ToList());

            }
            catch
            {
                throw;
            }
        }

        public async Task<ClienteDTO> Crear(ClienteDTO modelo)
        {
            try
            {
                var ClienteCreado = await _ClienteRepositorio.Crear(_mapper.Map<Cliente>(modelo));

                if (ClienteCreado.IdCliente == 0)
                    throw new TaskCanceledException("No se pudo crear");

                return _mapper.Map<ClienteDTO>(ClienteCreado);

            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Editar(ClienteDTO modelo)
        {
            try
            {

                var ClienteModelo = _mapper.Map<Cliente>(modelo);
                var ClienteEncontrado = await _ClienteRepositorio.Obtener(u =>
                    u.IdCliente == ClienteModelo.IdCliente
                    );

                if (ClienteEncontrado == null)
                    throw new TaskCanceledException("El Cliente no existe");


                ClienteEncontrado.Nombres = ClienteModelo.Nombres;
                ClienteEncontrado.Apellidos = ClienteModelo.Apellidos;
                ClienteEncontrado.Telefono = ClienteModelo.Telefono;
                ClienteEncontrado.Correo = ClienteModelo.Correo;
                ClienteEncontrado.Direccion = ClienteModelo.Direccion;
                bool respuesta = await _ClienteRepositorio.Editar(ClienteEncontrado);

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

                var ClienteEncontrado = await _ClienteRepositorio.Obtener(p => p.IdCliente == id);

                if (ClienteEncontrado == null)
                    throw new TaskCanceledException("El cliente no existe");

                bool respuesta = await _ClienteRepositorio.Eliminar(ClienteEncontrado);


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

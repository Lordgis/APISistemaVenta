using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.API.Utilidad;
using SistemaVenta.DAL.dbcontext;

namespace SistemaVenta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReparacionesController : ControllerBase
    {
        private readonly IReparacionService _reparacionService;

        public ReparacionesController(IReparacionService reparacionService)
        {
            _reparacionService = reparacionService;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var rsp = new Response<List<ReparacionesDTO>>();

            try
            {
                rsp.status = true;
                rsp.value = await _reparacionService.Lista();

            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;

            }
            return Ok(rsp);
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] ReparacionesDTO reparaciones)
        {
            var rsp = new Response<ReparacionesDTO>();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                rsp.status = true;
                rsp.value = await _reparacionService.Crear(reparaciones);

            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;

            }
            return Ok(rsp);
        }

        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] ReparacionesDTO reparaciones)
        {
            var rsp = new Response<bool>();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                rsp.status = true;
                rsp.value = await _reparacionService.Editar(reparaciones);

            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;

            }
            return Ok(rsp);
        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var rsp = new Response<bool>();

            try
            {
                rsp.status = true;
                rsp.value = await _reparacionService.Eliminar(id);

            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;

            }
            return Ok(rsp);

        }
    }
}

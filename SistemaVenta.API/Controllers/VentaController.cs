using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.API.Utilidad;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SistemaVenta.BLL.Servicios;
using SistemaVenta.Model;

namespace SistemaVenta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        private readonly IVentaService _ventaServicio;

        public VentaController(IVentaService ventaServicio)
        {
            _ventaServicio = ventaServicio;
        }

        [HttpPost]
        [Route("Registrar")]
        public async Task<IActionResult> Registrar([FromBody] VentaDTO venta)
        {
            var rsp = new Response<VentaDTO>();

            try
            {
                rsp.status = true;
                rsp.value = await _ventaServicio.Registrar(venta);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }

            return Ok(rsp);
        }

        [HttpGet]
        [Route("Historial")]
        public async Task<IActionResult> Historial(string buscarPor, string? numeroVenta, string? fechaInicio, string? fechaFin, string? dia)
        {
            var rsp = new Response<List<VentaDTO>>();

            numeroVenta = numeroVenta ?? "";
            fechaInicio = fechaInicio ?? "";
            fechaFin = fechaFin ?? "";
            dia = dia ?? "";

            try
            {
                rsp.status = true;
                rsp.value = await _ventaServicio.Historial(buscarPor, numeroVenta, fechaInicio, fechaFin, dia);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }

            return Ok(rsp);
        }

        [HttpGet]
        [Route("Reporte")]
        public async Task<IActionResult> Reporte(string? fechaInicio, string? fechaFin, string? producto, string? tipoPago)
        {
            var rsp = new Response<List<ReporteDTO>>();

            try
            {
                rsp.status = true;
                rsp.value = await _ventaServicio.Reporte(fechaInicio, fechaFin, producto, tipoPago);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }

            return Ok(rsp);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarVenta(int id)
        {
            try
            {
                await _ventaServicio.Eliminar(id);
                return Ok(new { status = true, message = "Venta eliminada correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { status = false, message = $"Error al eliminar la venta: {ex.Message}" });
            }
        }
    }
}

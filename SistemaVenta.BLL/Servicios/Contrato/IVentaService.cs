using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaVenta.DTO;
using SistemaVenta.Model;

namespace SistemaVenta.BLL.Servicios.Contrato
{
    public  interface IVentaService
    {

        Task<VentaDTO> Registrar(VentaDTO modelo);
        Task<List<VentaDTO>> Historial(string buscarPor, string numeroVenta, string fechaInicio, string fechaFin, string dia);
        Task<List<ReporteDTO>> Reporte(string fechaInicio, string fechaFin, string producto, string tipoPago);

        Task Eliminar(int id);
       
    }
}

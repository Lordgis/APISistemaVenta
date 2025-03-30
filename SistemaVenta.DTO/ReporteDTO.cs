using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.DTO
{
    public class ReporteDTO
    {
        public int IdDetalleVenta { get; set; }
        public int IdVenta { get; set; }
        public int IdProducto { get; set; }
        public string NumeroDocumento { get; set; }
        public string TotalVenta { get; set; }
        public string Precio { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Total { get; set; }
        public string Producto { get; set; }
        public string TipoPago { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}

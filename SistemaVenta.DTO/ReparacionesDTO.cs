using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.DTO
{
    public class ReparacionesDTO
    {
        public int IdReparaciones { get; set; }
        public string? Nombre { get; set; }
        public int? IdCategoria { get; set; }
        public string? DescripcionCategoria { get; set; }
        public string? precio { get; set; }
        public object Precio { get; set; }
        public DateTime? FechaRegistro { get; set; }

    }
}

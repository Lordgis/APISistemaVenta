using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.DTO
{
    public class ProveedorDTO
    {
        public int IdProveedor { get; set; }

        public string? NombreEmpresa { get; set; }

        public string? ContactoPersona { get; set; }

        public string? Telefono { get; set; }

        public string? Correo { get; set; }

        public string? Direccion { get; set; }

    }
}

using System;
using System.Collections.Generic;

namespace SistemaVenta.Model;

public  class Proveedor
{
    public int IdProveedor { get; set; }

    public string? NombreEmpresa { get; set; }

    public string? ContactoPersona { get; set; }

    public string? Telefono { get; set; }

    public string? Correo { get; set; }

    public string? Direccion { get; set; }

    public bool? EsActivo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<Producto> Productos { get; } = new List<Producto>();
}

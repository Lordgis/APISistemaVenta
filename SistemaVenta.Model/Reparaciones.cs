using System;
using System.Collections.Generic;

namespace SistemaVenta.Model;

public  class Reparaciones
{
    public int IdReparacion { get; set; }

    public int? IdCategoria { get; set; }

    public string? Nombre { get; set; }

    public decimal? Precio { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<DetalleVenta> DetalleVenta { get; } = new List<DetalleVenta>();

    public virtual Categoria? IdCategoriaNavigation { get; set; }
}

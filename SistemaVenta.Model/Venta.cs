﻿using System;
using System.Collections.Generic;

namespace SistemaVenta.Model;

public  class Venta
{
    public int IdVenta { get; set; }

    public string? NumeroDocumento { get; set; }

    public string? TipoPago { get; set; }

    public decimal? Total { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<DetalleVenta> DetalleVenta { get; } = new List<DetalleVenta>();
    //public int IdClienteVenta { get; set; }
   // public virtual Cliente? IdClienteVentaNavigation { get; set; }
}

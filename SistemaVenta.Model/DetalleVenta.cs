﻿using System;
using System.Collections.Generic;

namespace SistemaVenta.Model;

public partial class DetalleVenta
{
    public int IdDetalleVenta { get; set; }

    public int? IdVenta { get; set; }

    public int? IdProducto { get; set; }

    public int? Cantidad { get; set; }

    public decimal? Precio { get; set; }

    public decimal? Total { get; set; }

    public int? IdReparacion { get; set; }

    public int? IdCliente { get; set; }

    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }

    public virtual Reparaciones? IdReparacionNavigation { get; set; }

    public virtual Venta? IdVentaNavigation { get; set; }
}

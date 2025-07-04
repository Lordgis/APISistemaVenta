﻿using System;
using System.Collections.Generic;

namespace SistemaVenta.Model;

public  class Categoria
{
    public int IdCategoria { get; set; }

    public string? Nombre { get; set; }

    public bool? EsActivo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<Producto> Productos { get; } = new List<Producto>();

    public virtual ICollection<Reparaciones> Reparaciones { get; } = new List<Reparaciones>();
}

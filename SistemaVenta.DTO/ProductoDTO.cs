﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.DTO
{
    public class ProductoDTO
    {
        public int IdProducto { get; set; }
        public string? Nombre { get; set; }
        public int? IdCategoria { get; set; }
        public string? DescripcionCategoria { get; set; }
        public int? Stock { get; set; }
        public decimal? Precio { get; set; }
        public decimal? PrecioVenta { get; set; }
        public int? IdProveedor { get; set; }
        public string? DescripcionProveedor { get; set; }
        public int? EsActivo { get; set; }

    }

}

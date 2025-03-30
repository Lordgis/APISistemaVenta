using System;
using System.Collections.Generic;

namespace SistemaVenta.Model;

public class VentasEliminada
{
    public int Id { get; set; }

    public int IdVenta { get; set; }

    public DateTime FechaEliminacion { get; set; }

    public string MotivoEliminacion { get; set; } = null!;

    public string DetallesVenta { get; set; } = null!;
}

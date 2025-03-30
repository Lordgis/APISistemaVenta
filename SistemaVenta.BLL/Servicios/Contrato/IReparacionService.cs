using SistemaVenta.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.BLL.Servicios.Contrato
{
    public interface IReparacionService
    {
        Task<List<ReparacionesDTO>> Lista();
        Task<ReparacionesDTO> Crear(ReparacionesDTO modelo);
        Task<bool> Editar(ReparacionesDTO modelo);
        Task<bool> Eliminar(int id);
    }
}

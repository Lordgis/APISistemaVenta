using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaVenta.DAL.dbcontext;
using SistemaVenta.Model;

namespace SistemaVenta.DAL.Repositorios.Contrato
{
    public interface IVentaRepository : IGenericRepository<Venta>
    {
        Task<Venta> Registrar(Venta modelo);
        Task<bool> Eliminar(int idVenta);

        Task<Venta> ObtenerPorId(int id);
    }
}

using FrontVentasUsuarioFinal.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrontVentasUsuarioFinal.Consumidor
{
    public interface IConsumidorCatalogos
    {

        Task<IEnumerable<ClienteViewModel>> ObtenerClientes();

        Task<IEnumerable<EstatusViewModel>> ObtenerEstatus();

        Task<IEnumerable<SucursalViewModel>> ObtenerSucursales();
    }
}
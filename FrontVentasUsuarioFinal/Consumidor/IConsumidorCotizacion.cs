using FrontVentasUsuarioFinal.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrontVentasUsuarioFinal.Consumidor
{
    public interface IConsumidorCotizacion
    {
        Task<IEnumerable<CotizacionViewModel>> ListarCotizacionesFiltros(DateTime FechaInicial, DateTime Fechafinal, int IdSucursal, int Estatus);

        Task<IEnumerable<CotizacionViewModel>> ListarCotizacionesDelMes();

        Task<KeyValuePair<int, string>> NuevaCotizacion(CotizacionViewModel NuevaCotizacion);

        Task<KeyValuePair<int, string>> ActualizarCotizacion(int IdCotizacion, int Estatus, string MotivoRechazo, string Observaciones, string Usuario);

        Task<CotizacionViewModel> VerCotizacion(int IdCotizacion);

        Task<CotizacionViewModel> ObtenerCotizacionUnica(string ClaveCotizacion);
    }
}

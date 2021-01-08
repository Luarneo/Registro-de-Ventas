using APIVentasUsuarioFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIVentasUsuarioFinal.Repositorios
{
    interface IRepositorioCotizacion
    {
        List<Cotizacion> ListarCotizacionesFiltros(DateTime FechaInicial, DateTime Fechafinal, int IdSucursal, int Estatus);

        List<Cotizacion> ListarCotizacionesDelMes();

        KeyValuePair<int, string> NuevaCotizacion(Cotizacion NuevaCotizacion);

        KeyValuePair<int, string> ActualizarCotizacion(int IdCotizacion, int Estatus, string MotivoRechazo, string Observaciones, string Usuario);

        Cotizacion VerCotizacion(int IdCotizacion);

        Cotizacion ObtenerCotizacionUnica(string Cotizacion);

    }
}

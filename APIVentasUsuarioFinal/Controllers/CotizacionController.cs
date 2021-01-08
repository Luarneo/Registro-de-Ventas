using APIVentasUsuarioFinal.Models;
using APIVentasUsuarioFinal.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIVentasUsuarioFinal.Controllers
{
    public class CotizacionController : ApiController
    {

        private IRepositorioCotizacion _Repositorio;

        public CotizacionController()
        {
            _Repositorio = new RepositorioCotizacion();
        }


        [HttpPost]
        public HttpResponseMessage PostCotizacion(Cotizacion NuevaCotizacion)
        {
            try
            {
                KeyValuePair<int, string> respuestaRepo = _Repositorio.NuevaCotizacion(NuevaCotizacion);

                if (respuestaRepo.Key == 1)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, respuestaRepo);
                }

                throw new Exception(respuestaRepo.Value);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public HttpResponseMessage PutActualizarCotizacion(int IdCotizacion, int Estatus, string MotivoRechazo, string Observaciones, string UsuarioActualizacion)
        {
            try
            {
                KeyValuePair<int, string> respuestaRepo = _Repositorio.ActualizarCotizacion(IdCotizacion, Estatus, MotivoRechazo, Observaciones, UsuarioActualizacion);

                if (respuestaRepo.Key == 1)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, respuestaRepo);
                }

                throw new Exception(respuestaRepo.Value);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }



        [HttpGet]
        public HttpResponseMessage GetCotizacion(int IdCotizacion)
        {

            Cotizacion RespuestaCotizacion = new Cotizacion();
            RespuestaCotizacion = _Repositorio.VerCotizacion(IdCotizacion);

            if (RespuestaCotizacion.IdCliente > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, RespuestaCotizacion);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new Cotizacion().Observaciones = "No se encontro la cotización solicitada");
            }

        }

        [HttpGet]
        public HttpResponseMessage GetCotizacionUnica(string ClaveCotizacion)
        {

            Cotizacion RespuestaCotizacion = new Cotizacion();
            RespuestaCotizacion = _Repositorio.ObtenerCotizacionUnica(ClaveCotizacion);

            return Request.CreateResponse(HttpStatusCode.OK, RespuestaCotizacion);

            //if (RespuestaCotizacion.IdCliente > 0)
            //{
            //    return Request.CreateResponse(HttpStatusCode.OK, RespuestaCotizacion);
            //}
            //else
            //{
            //    return Request.CreateResponse(HttpStatusCode.NotFound, new Cotizacion().Observaciones = "No se encontro la cotización solicitada");
            //}

        }


        [HttpGet]
        public HttpResponseMessage GetListaCotizaciones(DateTime FechaInicial, DateTime Fechafinal, int IdSucursal, int Estatus)
        {

            List<Cotizacion> RespuestaCotizacion = new List<Cotizacion>();
            RespuestaCotizacion = _Repositorio.ListarCotizacionesFiltros(FechaInicial, Fechafinal, IdSucursal, Estatus);

            if (RespuestaCotizacion.Count > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, RespuestaCotizacion);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new List<Cotizacion>() { new Cotizacion() { Observaciones = "No se obtuvo ningún resultado con los argumentos indicados" } });
            }

        }

        [HttpGet]
        public HttpResponseMessage GetListaCotizacionesMes()
        {
            List<Cotizacion> RespuestaCotizacion = new List<Cotizacion>();
            RespuestaCotizacion = _Repositorio.ListarCotizacionesDelMes();

            if (RespuestaCotizacion.Count > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, RespuestaCotizacion);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new List<Cotizacion>() { new Cotizacion() { Observaciones = "No se obtuvo ningún resultado con los argumentos indicados" } });
            }
        }
    }
}

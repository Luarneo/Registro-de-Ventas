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
    public class CatalogosController : ApiController
    {

        private IRepositorioCatalogos _Repositorio;

        public CatalogosController()
        {
            _Repositorio = new RepositorioCatalogos();
        }


        [HttpGet]

        public HttpResponseMessage Get(int filtro)
        {
            if (filtro == 1)
            {
                try
                {
                    var ListaFinal = new List<Cliente>();

                    ListaFinal = _Repositorio.ObtenerClientes();


                    if (ListaFinal.Any())
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, ListaFinal);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound);
                    }

                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
                }
            }

            else if (filtro == 2)
            {
                try
                {
                    var ListaFinal = new List<Estatus>();

                    ListaFinal = _Repositorio.ObtenerEstatus();


                    if (ListaFinal.Any())
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, ListaFinal);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound);
                    }

                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
                }
            }
            else if (filtro == 3)
            {
                try
                {
                    var ListaFinal = new List<Sucursal>();

                    ListaFinal = _Repositorio.ObtenerSucursales();


                    if (ListaFinal.Any())
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, ListaFinal);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound);
                    }

                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
                }

            }

            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "no se ingreso un número de filtro existente");

            }

        }
    }
}

using FrontVentasUsuarioFinal.Consumidor;
using FrontVentasUsuarioFinal.Models;
using Microsoft.AspNet.Identity;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FrontVentasUsuarioFinal.Controllers
{
    public class CotizacionesController : Controller
    {

        private IConsumidorCotizacion _ServicioCotizaciones;
        private IConsumidorCatalogos _ServicioCatalogos;

       

        public CotizacionesController() : this(new ConsumidorCotizacion(), new ConsumidorCatalogos()) { }


        public CotizacionesController(IConsumidorCotizacion servicio, IConsumidorCatalogos servicio2)
        {
            this._ServicioCotizaciones = servicio;
            this._ServicioCatalogos = servicio2;
        }

        // GET: Cotizaciones
        [Authorize]
        public async Task<ActionResult> Index()
        {
            ViewBag.ListaSucursales = await _ServicioCatalogos.ObtenerSucursales();
            ViewBag.ListaEstatus = await _ServicioCatalogos.ObtenerEstatus();

            //var CotizacionesMes = await _ServicioCotizaciones.ListarCotizacionesDelMes();

           

            //return View(CotizacionesMes);
            return View();
        }

        [Authorize]
        public async Task<JsonResult> MostrarCotizaciones(DateTime FechaInicial, DateTime FechaFinal, int Sucursal, int Estatus)
        {
            IEnumerable<CotizacionViewModel> contenido = new List<CotizacionViewModel>();
            
            if (Sucursal == 0 && Estatus == 0)
            {
                contenido = await _ServicioCotizaciones.ListarCotizacionesDelMes();
            }
            else
            {
                contenido = await _ServicioCotizaciones.ListarCotizacionesFiltros(FechaInicial, FechaFinal, Sucursal, Estatus);
            }
                     
            return Json(contenido, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> MostrarCotizacionUnica(string ClaveCotizacion)
        {
            CotizacionViewModel contenido = new CotizacionViewModel();

            contenido = await _ServicioCotizaciones.ObtenerCotizacionUnica(ClaveCotizacion);   
            
            if(contenido.IdCotizacion == 0)
            {
                return new JsonResult()
                {
                    Data = new {IdNoexiste= contenido.IdEstatus, MensajeNoExiste = contenido.Observaciones }
                };
            }
            else
            {
                return Json(contenido, JsonRequestBehavior.AllowGet);
            }


            //var cadFechaInicial = FechaInicial.ToString();
            //var cadFechaFinal = FechaFinal.ToString();

            //return new JsonResult()
            //{
            //    Data = new { FInicial = cadFechaInicial, FFinal = cadFechaFinal, Suc = Sucursal, Est = Estatus, Cot = Cotizacion }
            //};

        }



        [Authorize]
        public async Task<ActionResult> Cotizacion()
        {

            ViewBag.ListaSucursales = await _ServicioCatalogos.ObtenerSucursales();
            ViewBag.ListaEstatus = await _ServicioCatalogos.ObtenerEstatus();
            ViewBag.Clientes = await _ServicioCatalogos.ObtenerClientes();

            return View();

        }

        [HttpPost]
        public async Task<ActionResult> Cotizacion(CotizacionViewModel Cotizacion)
        {

            if (Cotizacion.IdCliente is null)
            {
                Cotizacion.IdCliente = 18;
            }

            Cotizacion.IdEstatus = 3;

            Cotizacion.Usuario = User.Identity.GetUserName();

            KeyValuePair<int, string> respuesta = new KeyValuePair<int, string>();

            respuesta = await _ServicioCotizaciones.NuevaCotizacion(Cotizacion);

            if (respuesta.Value.Contains("Operación Exitosa"))
            {
                TempData["Mensaje"] = new KeyValuePair<string, string>("success", "La cotización se ha registrado correctamente");

            }
            else
            {
                TempData["Mensaje"] = new KeyValuePair<string, string>("danger", "Error al registrar cotización");
            }

            return RedirectToAction("Index");

        }

        //public async Task<ActionResult> VerCotizacion(int CotizacionId)
        //{
        //    var CotizacionEncontrada = await _ServicioCotizaciones.VerCotizacion(CotizacionId);

        //    return View(CotizacionEncontrada);
        //}

        public async Task<PartialViewResult> VerCotizacion(int CotizacionId)
        {
            var CotizacionEncontrada = await _ServicioCotizaciones.VerCotizacion(CotizacionId);
                       
            return PartialView("_VerCotizacion", CotizacionEncontrada);
        }




        public async Task<PartialViewResult> ActualizarCotizacion(int CotizacionId)
        {

            var CotizacionEncontrada = await _ServicioCotizaciones.VerCotizacion(CotizacionId);

            var listaEstatus = await _ServicioCatalogos.ObtenerEstatus();

            ViewBag.ListaEstatus = listaEstatus.Where(a=>a.IdEstatus != 3);

            return PartialView("_ActualizarCotizacion", CotizacionEncontrada);          

        }

        [HttpPost]
        public async Task<ActionResult> ActualizarCotizacion(CotizacionViewModel Cotizacion)
        {

            Cotizacion.Usuario = User.Identity.GetUserName();

            var ResultadoOperacion = await _ServicioCotizaciones.ActualizarCotizacion(
                Cotizacion.IdCotizacion,
                Cotizacion.IdEstatus,
                Cotizacion.MotivoRechazo,
                Cotizacion.Observaciones,
                Cotizacion.Usuario);

            if (ResultadoOperacion.Value.Contains("Operación Exitosa"))
            {
                TempData["Mensaje"] = new KeyValuePair<string, string>("success", "La actualización se ejecuto correctamente");

            }
            else
            {
                TempData["Mensaje"] = new KeyValuePair<string, string>("danger", "Error al actualizar cotización");
            }

            return RedirectToAction("Index");

        }


       
       

        public JsonResult ObtenerDatosParaExcel(DateTime FechaInicial, DateTime FechaFinal, int Sucursal, int Estatus, string Cotizacion)
        {
            var cadFechaInicial = FechaInicial.ToString();
            var cadFechaFinal = FechaFinal.ToString();

            return new JsonResult()
            {
                Data = new { FInicial = cadFechaInicial, FFinal = cadFechaFinal, Suc = Sucursal, Est = Estatus, Cot = Cotizacion}
            };
        }

      
        public async Task<ActionResult> CrearExcel(string FechaInicial, string FechaFinal, int Sucursal, int Estatus, string Cotizacion)
        {
                
                      

            if (Cotizacion != "")
            {
                var CotizacionUnica = new CotizacionViewModel();

                CotizacionUnica = await _ServicioCotizaciones.ObtenerCotizacionUnica(Cotizacion);

                ExcelPackage ep = new ExcelPackage();

                ep.Workbook.Worksheets.Add("Cotizaciones");

                ExcelWorksheet ew1 = ep.Workbook.Worksheets[1];

                ew1.Cells["A1"].Value = "Fecha";
                ew1.Cells["A1"].Style.Font.Bold = true;           
                ew1.Cells["B1"].Value = "Sucursal";
                ew1.Cells["B1"].Style.Font.Bold = true;
                ew1.Cells["C1"].Value = "Cliente";
                ew1.Cells["C1"].Style.Font.Bold = true;
                ew1.Cells["D1"].Value = "Descripción";
                ew1.Cells["D1"].Style.Font.Bold = true;
                ew1.Cells["E1"].Value = "Estatus";
                ew1.Cells["E1"].Style.Font.Bold = true;

                int rowStart = 2;

                ew1.Cells[string.Format("A{0}", rowStart)].Value = CotizacionUnica.Fecha;
                ew1.Cells[string.Format("B{0}", rowStart)].Value = CotizacionUnica.Sucursal;
                ew1.Cells[string.Format("C{0}", rowStart)].Value = CotizacionUnica.Cliente;
                ew1.Cells[string.Format("D{0}", rowStart)].Value = CotizacionUnica.DescripcionCotizacion;
                ew1.Cells[string.Format("E{0}", rowStart)].Value = CotizacionUnica.Estatus;

                ew1.Column(1).AutoFit();
                ew1.Column(2).AutoFit();
                ew1.Column(3).AutoFit();
                ew1.Column(4).AutoFit();
                ew1.Column(5).AutoFit();

                byte[] bin = ep.GetAsByteArray();

                Response.ClearHeaders();
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet";
                Response.AddHeader("content-length", bin.Length.ToString());
                Response.AddHeader("content-disposition", "attachment; filename=\"Cotizaciones.xlsx\"");
                Response.OutputStream.Write(bin, 0, bin.Length);
                Response.Flush();
                                
            }

            
            else
            {
                DateTime FechaInicialConv = Convert.ToDateTime(FechaInicial);
                DateTime FechaFinalConv = Convert.ToDateTime(FechaFinal);

                IEnumerable<CotizacionViewModel> CotizacionList = new List<CotizacionViewModel>();

                if (Sucursal == 0 && Estatus == 0)
                {
                    CotizacionList = await _ServicioCotizaciones.ListarCotizacionesDelMes();
                }
                else
                {
                    CotizacionList = await _ServicioCotizaciones.ListarCotizacionesFiltros(FechaInicialConv, FechaFinalConv, Sucursal, Estatus);
                }





                ExcelPackage ep = new ExcelPackage();

                ep.Workbook.Worksheets.Add("Cotizaciones");

                ExcelWorksheet ew1 = ep.Workbook.Worksheets[1];

                ew1.Cells["A1"].Value = "Fecha";
                ew1.Cells["A1"].Style.Font.Bold = true;
                ew1.Cells["B1"].Value = "Sucursal";
                ew1.Cells["B1"].Style.Font.Bold = true;
                ew1.Cells["C1"].Value = "Cliente";
                ew1.Cells["C1"].Style.Font.Bold = true;
                ew1.Cells["D1"].Value = "Descripción";
                ew1.Cells["D1"].Style.Font.Bold = true;
                ew1.Cells["E1"].Value = "Estatus";
                ew1.Cells["E1"].Style.Font.Bold = true;



                int rowStart = 2;

                    foreach (var item in CotizacionList)
                    {
                    ew1.Cells[string.Format("A{0}", rowStart)].Value = item.Fecha;
                    ew1.Cells[string.Format("B{0}", rowStart)].Value = item.Sucursal;
                    ew1.Cells[string.Format("C{0}", rowStart)].Value = item.Cliente;
                    ew1.Cells[string.Format("D{0}", rowStart)].Value = item.DescripcionCotizacion;
                    ew1.Cells[string.Format("E{0}", rowStart)].Value = item.Estatus;

                        rowStart++;

                    }

                ew1.Column(1).AutoFit();
                ew1.Column(2).AutoFit();
                ew1.Column(3).AutoFit();
                ew1.Column(4).AutoFit();
                ew1.Column(5).AutoFit();

                byte[] bin = ep.GetAsByteArray();

                Response.ClearHeaders();
                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-length", bin.Length.ToString());
                    Response.AddHeader("content-disposition", "attachment; filename=\"Cotizaciones.xlsx\"");
                    Response.OutputStream.Write(bin, 0, bin.Length);
                    Response.Flush();
                

            }           

            return RedirectToAction("Index");
        }

        
    }
}

using FrontVentasUsuarioFinal.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FrontVentasUsuarioFinal.Consumidor
{
    public class ConsumidorCotizacion : IConsumidorCotizacion
    {

        private string WebApiUrl = ConfigurationManager.AppSettings["UrlApi"];

        private HttpClient GetClient()
        {
            var cliente = new HttpClient
            {
                BaseAddress = new Uri(WebApiUrl),
            };

            cliente.DefaultRequestHeaders.Clear();
            cliente.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            return cliente;
        }

        private class Respuesta
        {
            public short Resultado { get; set; }
            public string Mensaje { get; set; }
        }

        public async Task<KeyValuePair<int, string>> ActualizarCotizacion(int IdCotizacion, int Estatus, string MotivoRechazo, string Observaciones, string Usuario)
        {


            try
            {

                using (var cliente = GetClient())
                {

                    var json = JsonConvert.SerializeObject(new
                    {
                        IdCotizacion,
                        Estatus,
                        MotivoRechazo,
                        Observaciones,
                        Usuario
                    });

                    HttpResponseMessage respuesta = await cliente.PutAsJsonAsync($"api/Cotizacion?IdCotizacion={IdCotizacion}&Estatus={Estatus}&MotivoRechazo={MotivoRechazo}&Observaciones={Observaciones}&UsuarioActualizacion={Usuario}", new StringContent(json, Encoding.UTF8, "application/json"));

                    var content = respuesta.Content.ReadAsStringAsync();
                    var id = JsonConvert.DeserializeObject<Respuesta>(await content);

                    return new KeyValuePair<int, string>(id.Resultado, content.Result);
                }

            }
            catch (Exception e)
            {
                return new KeyValuePair<int, string>(-1, $"Error interno: {e}");
            }
        }

        public async Task<IEnumerable<CotizacionViewModel>> ListarCotizacionesFiltros(DateTime FechaInicial, DateTime Fechafinal, int IdSucursal, int Estatus)
        {

            //Cambio de formato de fechas
            var DiaFechaInicial = FechaInicial.Day;
            var MesFechaInicial = FechaInicial.Month;
            var AnioFechaInicial = FechaInicial.Year;
            var FechaInicialOk = AnioFechaInicial + "-" + MesFechaInicial + "-" + DiaFechaInicial;

            var DiaFechaFinal = Fechafinal.Day;
            var MesFechaFinal = Fechafinal.Month;
            var AnioFechaFinal = Fechafinal.Year;
            var FechaFinalOk = AnioFechaFinal + "-" + MesFechaFinal + "-" + DiaFechaFinal;


            HttpResponseMessage resp;
            using (var cliente = GetClient())
            {
                resp = await cliente.GetAsync($"api/Cotizacion?FechaInicial={FechaInicialOk}&Fechafinal={FechaFinalOk}&IdSucursal={IdSucursal}&Estatus={Estatus}");
            }

            if (resp.IsSuccessStatusCode)
            {
                var json = resp.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<CotizacionViewModel[]>(await json);
            }

            return new List<CotizacionViewModel>();
        }

        public async Task<IEnumerable<CotizacionViewModel>> ListarCotizacionesDelMes()
        {
            HttpResponseMessage resp;
            using (var cliente = GetClient())
            {
                resp = await cliente.GetAsync($"api/Cotizacion");
            }

            if (resp.IsSuccessStatusCode)
            {
                var json = resp.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<CotizacionViewModel[]>(await json);
            }

            return new List<CotizacionViewModel>();
        }

        public async Task<KeyValuePair<int, string>> NuevaCotizacion(CotizacionViewModel NuevaCotizacion)
        {
            try
            {


                using (var cliente = GetClient())
                {
                    HttpResponseMessage respuesta = await cliente.PostAsJsonAsync("api/Cotizacion", NuevaCotizacion);

                    var content = respuesta.Content.ReadAsStringAsync();
                    var id = JsonConvert.DeserializeObject<Respuesta>(await content);

                    return new KeyValuePair<int, string>(id.Resultado, content.Result);
                }

            }
            catch (Exception e)
            {
                return new KeyValuePair<int, string>(-1, $"Error interno: {e}");
            }
        }

        public async Task<CotizacionViewModel> VerCotizacion(int IdCotizacion)
        {
            HttpResponseMessage resp;
            using (var cliente = GetClient())
            {
                resp = await cliente.GetAsync($"api/Cotizacion?IdCotizacion={IdCotizacion}");
            }

            if (resp.IsSuccessStatusCode)
            {
                var json = resp.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<CotizacionViewModel>(await json);
            }

            return new CotizacionViewModel();
        }

      
        public async Task<CotizacionViewModel> ObtenerCotizacionUnica(string ClaveCotizacion)
        {
            HttpResponseMessage resp;
            using (var cliente = GetClient())
            {
                resp = await cliente.GetAsync($"api/Cotizacion?ClaveCotizacion={ClaveCotizacion}");
            }

            if (resp.IsSuccessStatusCode)
            {
                var json = resp.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<CotizacionViewModel>(await json);
            }

            return new CotizacionViewModel();
        }
    }
}
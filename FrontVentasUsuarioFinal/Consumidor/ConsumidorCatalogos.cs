using FrontVentasUsuarioFinal.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrontVentasUsuarioFinal.Consumidor
{
    public class ConsumidorCatalogos : IConsumidorCatalogos
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

        // <summary>
        /// Respuesta satisfactoria de una petición HTTP
        /// </summary>
        private class Respuesta
        {
            public short Resultado { get; set; }
            public string Mensaje { get; set; }
        }

        public async Task<IEnumerable<ClienteViewModel>> ObtenerClientes()
        {
            HttpResponseMessage resp;
            using (var cliente = GetClient())
            {
                resp = await cliente.GetAsync($"api/Catalogos?Filtro=1");
            }

            if (resp.IsSuccessStatusCode)
            {
                var json = resp.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ClienteViewModel[]>(await json);
            }

            return new List<ClienteViewModel>();
        }

        public async Task<IEnumerable<EstatusViewModel>> ObtenerEstatus()
        {
            HttpResponseMessage resp;
            using (var cliente = GetClient())
            {
                resp = await cliente.GetAsync($"api/Catalogos?Filtro=2");
            }

            if (resp.IsSuccessStatusCode)
            {
                var json = resp.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<EstatusViewModel[]>(await json);
            }

            return new List<EstatusViewModel>();
        }

        public async Task<IEnumerable<SucursalViewModel>> ObtenerSucursales()
        {
            HttpResponseMessage resp;
            using (var cliente = GetClient())
            {
                resp = await cliente.GetAsync($"api/Catalogos?Filtro=3");
            }

            if (resp.IsSuccessStatusCode)
            {
                var json = resp.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<SucursalViewModel[]>(await json);
            }

            return new List<SucursalViewModel>();
        }
    }
}
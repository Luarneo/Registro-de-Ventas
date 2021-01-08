using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIVentasUsuarioFinal.Models
{
    public class Cotizacion
    {
       
        public int IdCotizacion { get; set; }

        public string Fecha { get; set; }
        //public DateTime Fecha { get; set; }

        public int IdSucursal { get; set; }

        public string Sucursal { get; set; }

        public string DescripcionCotizacion { get; set; }

        public int? IdCliente { get; set; }

        public string Cliente { get; set; }

        public double Importe { get; set; }

        public int IdEstatus { get; set; }

        public string Estatus { get; set; }

        public string Observaciones { get; set; }

        public string Usuario { get; set; }
    }
}
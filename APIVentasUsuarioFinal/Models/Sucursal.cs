using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIVentasUsuarioFinal.Models
{
    public class Sucursal
    {
        public int IdSucursal { get; set; }

        public string NombreSucursal { get; set; }

        public string DescipcionSucursal { get; set; }

        public DateTime CreacionSucursal { get; set; }
    }
}
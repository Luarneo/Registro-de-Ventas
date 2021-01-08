using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIVentasUsuarioFinal.Models
{
    public class Cliente
    {
        public int IdCliente { get; set; }

        public string NombreCliente { get; set; }

        public string DescripcionCliente { get; set; }

        public string ClaveCliente { get; set; }

        public DateTime CreacionCliente { get; set; }
    }
}
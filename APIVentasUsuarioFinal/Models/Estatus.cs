using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIVentasUsuarioFinal.Models
{
    public class Estatus
    {
        public int IdEstatus { get; set; }

        public string NombreEstatus { get; set; }

        public DateTime FechaCreacion { get; set; }
    }
}
using System;

namespace FrontVentasUsuarioFinal.Models
{
    public class ClienteViewModel
    {
        public int IdCliente { get; set; }

        public string NombreCliente { get; set; }

        public string DescripcionCliente { get; set; }

        public string ClaveCliente { get; set; }

        public DateTime CreacionCliente { get; set; }
    }
}
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FrontVentasUsuarioFinal.Models
{
    public class CotizacionViewModel
    {
        public int IdCotizacion { get; set; }

        //[DataType(DataType.Date)]
        //public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]       
        public string Fecha { get; set; }

        [DisplayName("Sucursal")]
        [Required(ErrorMessage = "El campo Sucursal es requerido")]
        public int IdSucursal { get; set; }

        public string Sucursal { get; set; }

        [DisplayName("Cotización")]
        [Required(ErrorMessage = "El campo Cotización es requerido")]
        public string DescripcionCotizacion { get; set; }

        [DisplayName("Cliente")]
        public int? IdCliente { get; set; }

        public string Cliente { get; set; }

        [DisplayFormat(DataFormatString = "{0:$#,#}")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public decimal Importe { get; set; }

        [DisplayName("Estatus")]
        [Required(ErrorMessage = "El campo Estatus es requerido")]
        public int IdEstatus { get; set; }

        public string Estatus { get; set; }

        [DisplayName("Motivo")]
        public string MotivoRechazo { get; set; }

        public string Observaciones { get; set; }

        public string Usuario { get; set; }
    }
}
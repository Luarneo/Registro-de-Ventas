using APIVentasUsuarioFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIVentasUsuarioFinal.Repositorios
{
    interface IRepositorioCatalogos
    {

        List<Estatus> ObtenerEstatus();

        List<Sucursal> ObtenerSucursales();

        List<Cliente> ObtenerClientes();
    }
}

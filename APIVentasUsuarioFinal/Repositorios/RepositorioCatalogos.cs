using APIVentasUsuarioFinal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace APIVentasUsuarioFinal.Repositorios
{
    public class RepositorioCatalogos : IRepositorioCatalogos
    {

        SqlConnection conexion = new SqlConnection("Data Source =.; initial catalog = BDVentaFinal; User ID = UserVentasFinal; Password=master172020;");

        public List<Cliente> ObtenerClientes()
        {
            List<Cliente> ListaFinal = new List<Cliente> ();
            string SPObtenerClientes = "[dbo].[SP_OBTENER_LISTA_CLIENTES]";

            SqlCommand cmd = new SqlCommand(SPObtenerClientes, conexion);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Cliente ClienteCot = new Cliente();

                ClienteCot.IdCliente = Convert.ToInt32(dt.Rows[i]["Id"]);
                ClienteCot.NombreCliente = dt.Rows[i]["nombre"].ToString();
                ClienteCot.DescripcionCliente = dt.Rows[i]["descripcion"].ToString();
                ClienteCot.ClaveCliente = dt.Rows[i]["clave_intelisis"].ToString();
                ClienteCot.CreacionCliente = Convert.ToDateTime(dt.Rows[i]["fecha_creacion"]);

                ListaFinal.Add(ClienteCot);

            }

            return ListaFinal;
        }

        public List<Estatus> ObtenerEstatus()
        {
            List<Estatus> ListaFinal = new List<Estatus>();
            string SPObtenerEstatus = "[dbo].[SP_OBTENER_ESTATUS_COTIZACIONES]";

            SqlCommand cmd = new SqlCommand(SPObtenerEstatus, conexion);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Estatus EstatusCot = new Estatus();

                EstatusCot.IdEstatus = Convert.ToInt32(dt.Rows[i]["Id"]);
                EstatusCot.NombreEstatus = dt.Rows[i]["nombre"].ToString();
                EstatusCot.FechaCreacion = Convert.ToDateTime(dt.Rows[i]["fecha_creacion"]);
            
                ListaFinal.Add(EstatusCot);

            }

            return ListaFinal;
        }

        public List<Sucursal> ObtenerSucursales()
        {
            List<Sucursal> ListaFinal = new List<Sucursal>();
            string SPObtenerSucursales= "[dbo].[SP_OBTENER_SUCURSALES]";

            SqlCommand cmd = new SqlCommand(SPObtenerSucursales, conexion);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Sucursal SucursalCot = new Sucursal();

                SucursalCot.IdSucursal = Convert.ToInt32(dt.Rows[i]["Id"]);
                SucursalCot.NombreSucursal = dt.Rows[i]["nombre"].ToString();
                SucursalCot.DescipcionSucursal = dt.Rows[i]["descripcion"].ToString();
                SucursalCot.CreacionSucursal = Convert.ToDateTime(dt.Rows[i]["fecha_creacion"]);

                ListaFinal.Add(SucursalCot);

            }

            return ListaFinal;
        }
    }
}
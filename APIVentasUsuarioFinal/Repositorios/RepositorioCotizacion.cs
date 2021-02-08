using APIVentasUsuarioFinal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace APIVentasUsuarioFinal.Repositorios
{
    public class RepositorioCotizacion : IRepositorioCotizacion
    {

        SqlConnection conexion = new SqlConnection("Data Source =.; initial catalog = BDVentaFinal; User ID = UserVentasFinal; Password=master172020;");
        public KeyValuePair<int, string> ActualizarCotizacion(int IdCotizacion, int Estatus, string MotivoRechazo, string Observaciones, string Usuario)
        {
            try
            {
                string SPActCot = "dbo.[SP_ACTUALIZAR_COTIZACION]";

                SqlCommand cmd = new SqlCommand(SPActCot, conexion);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdCotizacion", IdCotizacion);
                cmd.Parameters.AddWithValue("@Estatus", Estatus);
                cmd.Parameters.AddWithValue("@MotivoRechazo", MotivoRechazo);
                cmd.Parameters.AddWithValue("@Observaciones", Observaciones);
                cmd.Parameters.AddWithValue("@UsuarioActualizacion", Usuario);


                conexion.Open();
                cmd.ExecuteNonQuery();
                conexion.Close();

                return new KeyValuePair<int, string>(1, "Operación Exitosa");
            }
            catch (Exception ex)
            {
                return new KeyValuePair<int, string>(0, ex.Message);
            }
        }

        public List<Cotizacion> ListarCotizacionesDelMes()
        {
            /// CAMBIAR FECHA INICIAL Y FECHA FINAL POR FECHAS DINAMICAS
            /// 

            
            DateTime date = DateTime.Now;            
            DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);
            DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.AddMonths(1).AddDays(-1);


            var FechaInicial = oPrimerDiaDelMes;
            var FechaFinal = oUltimoDiaDelMes;


            List<Cotizacion> ListaFinal = new List<Cotizacion>();
            string SPObtenerCotMes = "[dbo].[SP_OBTENER_COTIZACIONES_MES]";

            SqlCommand cmd = new SqlCommand(SPObtenerCotMes, conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FechaInicio", FechaInicial);
            cmd.Parameters.AddWithValue("@FechaFinal", FechaFinal);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Cotizacion ElemCotizacion = new Cotizacion();

                ElemCotizacion.IdCotizacion = Convert.ToInt32(dt.Rows[i]["Id"]);
                //ElemCotizacion.Fecha = Convert.ToDateTime(dt.Rows[i]["fecha_cotizacion"]);
                var FechaInicial1 = Convert.ToDateTime(dt.Rows[i]["fecha_cotizacion"]);
                ElemCotizacion.Fecha = FechaInicial1.ToString("yyyy-MM-dd");
                ElemCotizacion.IdSucursal = Convert.ToInt32(dt.Rows[i]["sucursal_id"]);
                ElemCotizacion.Sucursal = dt.Rows[i]["Nombre_sucursal"].ToString();
                ElemCotizacion.DescripcionCotizacion = dt.Rows[i]["descripcion_cotizacion"].ToString();
                ElemCotizacion.IdCliente = Convert.ToInt32(dt.Rows[i]["cliente_id"]);
                ElemCotizacion.Cliente = dt.Rows[i]["Nombre_cliente"].ToString();
                ElemCotizacion.Importe = Convert.ToDecimal(dt.Rows[i]["importe"]);
                ElemCotizacion.IdEstatus = Convert.ToInt32(dt.Rows[i]["estatus"]);
                ElemCotizacion.Estatus = dt.Rows[i]["nombre_estatus"].ToString();
                ElemCotizacion.Observaciones = dt.Rows[i]["observaciones"].ToString();

                ListaFinal.Add(ElemCotizacion);

            }

            return ListaFinal;
        }

        public List<Cotizacion> ListarCotizacionesFiltros(DateTime FechaInicial, DateTime FechaFinal, int IdSucursal, int Estatus)
        {

            List<Cotizacion> ListaFinal = new List<Cotizacion>();
            string SPObtenerCotMes = "[dbo].[SP_OBTENER_COTIZACIONES_FILTROS]";

            SqlCommand cmd = new SqlCommand(SPObtenerCotMes, conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FechaInicio", FechaInicial);
            cmd.Parameters.AddWithValue("@FechaFinal", FechaFinal);
            cmd.Parameters.AddWithValue("@IdSucursal", IdSucursal);
            cmd.Parameters.AddWithValue("@Estatus", Estatus);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Cotizacion ElemCotizacion = new Cotizacion();

                ElemCotizacion.IdCotizacion = Convert.ToInt32(dt.Rows[i]["Id"]);
                var FechaInicial1 = Convert.ToDateTime(dt.Rows[i]["fecha_cotizacion"]);
                ElemCotizacion.Fecha = FechaInicial1.ToString("yyyy-MM-dd");
                ElemCotizacion.IdSucursal = Convert.ToInt32(dt.Rows[i]["sucursal_id"]);
                ElemCotizacion.Sucursal = dt.Rows[i]["Nombre_sucursal"].ToString();
                ElemCotizacion.DescripcionCotizacion = dt.Rows[i]["descripcion_cotizacion"].ToString();
                ElemCotizacion.IdCliente = Convert.ToInt32(dt.Rows[i]["cliente_id"]);
                ElemCotizacion.Cliente = dt.Rows[i]["Nombre_cliente"].ToString();
                ElemCotizacion.Importe = Convert.ToDecimal(dt.Rows[i]["importe"]);
                ElemCotizacion.IdEstatus = Convert.ToInt32(dt.Rows[i]["estatus"]);
                ElemCotizacion.Estatus = dt.Rows[i]["nombre_estatus"].ToString();
                ElemCotizacion.Observaciones = dt.Rows[i]["observaciones"].ToString();

                ListaFinal.Add(ElemCotizacion);

            }

            return ListaFinal;
        }

        public KeyValuePair<int, string> NuevaCotizacion(Cotizacion NuevaCotizacion)
        {
           try
            {
                string SPInsertCot = "dbo.[SP_INSERTAR_NUEVA_COTIZACION]";

                SqlCommand cmd = new SqlCommand(SPInsertCot, conexion);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Fecha", NuevaCotizacion.Fecha);
                cmd.Parameters.AddWithValue("@IdSucursal", NuevaCotizacion.IdSucursal);
                cmd.Parameters.AddWithValue("@IdCliente", NuevaCotizacion.IdCliente);
                cmd.Parameters.AddWithValue("@Importe", NuevaCotizacion.Importe);
                cmd.Parameters.AddWithValue("@IdEstatus", NuevaCotizacion.IdEstatus);
                cmd.Parameters.AddWithValue("@Observaciones", NuevaCotizacion.Observaciones);
                cmd.Parameters.AddWithValue("@Usuario", NuevaCotizacion.Usuario);
                cmd.Parameters.AddWithValue("@Descripcion",NuevaCotizacion.DescripcionCotizacion);

                conexion.Open();
                cmd.ExecuteNonQuery();
                conexion.Close();

                return new KeyValuePair<int, string>(1, "Operación Exitosa");
            }
            catch (Exception ex)
            {
                return new KeyValuePair<int, string>(0, ex.Message);
            }
        }

        public Cotizacion VerCotizacion(int IdCotizacion)
        {
            
            string SPObtenerCotMes = "[dbo].[SP_OBTENER_COTIZACION]";

            SqlCommand cmd = new SqlCommand(SPObtenerCotMes, conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdCotizacion", IdCotizacion);
          

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

          
                Cotizacion ElemCotizacion = new Cotizacion();

                ElemCotizacion.IdCotizacion = Convert.ToInt32(dt.Rows[0]["Id"]);
                
                var FechaInicial1 = Convert.ToDateTime(dt.Rows[0]["fecha_cotizacion"]);
                ElemCotizacion.Fecha = FechaInicial1.ToString("yyyy-MM-dd");
                ElemCotizacion.IdSucursal = Convert.ToInt32(dt.Rows[0]["sucursal_id"]);
                ElemCotizacion.Sucursal = dt.Rows[0]["Nombre_sucursal"].ToString();
                ElemCotizacion.DescripcionCotizacion = dt.Rows[0]["descripcion_cotizacion"].ToString();
                ElemCotizacion.IdCliente = Convert.ToInt32(dt.Rows[0]["cliente_id"]);
                ElemCotizacion.Cliente = dt.Rows[0]["Nombre_cliente"].ToString();
                ElemCotizacion.Importe = Convert.ToDecimal(dt.Rows[0]["importe"]);
                ElemCotizacion.IdEstatus = Convert.ToInt32(dt.Rows[0]["estatus"]);
                ElemCotizacion.Estatus = dt.Rows[0]["nombre_estatus"].ToString();
                ElemCotizacion.Observaciones = dt.Rows[0]["observaciones"].ToString();
                ElemCotizacion.MotivoRechazo = dt.Rows[0]["motivo_rechazo"].ToString();


            return ElemCotizacion;
        }

        public Cotizacion ObtenerCotizacionUnica(string Cotizacion)
        {
            string SPObtenerCotMes = "[dbo].[SP_OBTENER_COTIZACION_UNICA]";

            SqlCommand cmd = new SqlCommand(SPObtenerCotMes, conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ClaveCotizacion", Cotizacion);


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            Cotizacion ElemCotizacion = new Cotizacion();

            if (dt.Rows.Count>0)
            {
                ElemCotizacion.IdCotizacion = Convert.ToInt32(dt.Rows[0]["Id"]);

                var FechaInicial1 = Convert.ToDateTime(dt.Rows[0]["fecha_cotizacion"]);
                ElemCotizacion.Fecha = FechaInicial1.ToString("yyyy-MM-dd");
                ElemCotizacion.IdSucursal = Convert.ToInt32(dt.Rows[0]["sucursal_id"]);
                ElemCotizacion.Sucursal = dt.Rows[0]["Nombre_sucursal"].ToString();
                ElemCotizacion.DescripcionCotizacion = dt.Rows[0]["descripcion_cotizacion"].ToString();
                ElemCotizacion.IdCliente = Convert.ToInt32(dt.Rows[0]["cliente_id"]);
                ElemCotizacion.Cliente = dt.Rows[0]["Nombre_cliente"].ToString();
                ElemCotizacion.Importe = Convert.ToDecimal(dt.Rows[0]["importe"]);
                ElemCotizacion.IdEstatus = Convert.ToInt32(dt.Rows[0]["estatus"]);
                ElemCotizacion.Estatus = dt.Rows[0]["nombre_estatus"].ToString();
                ElemCotizacion.Observaciones = dt.Rows[0]["observaciones"].ToString();
            }
            else
            {
                ElemCotizacion.IdEstatus = -1;
                ElemCotizacion.Observaciones = "La cotización " + Cotizacion + " no existe en la base de datos";
            }
            

            
            return ElemCotizacion;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
namespace PELUQUERIA.Clases
{
    public class cVenta
    {
        public Int32 InsertarVenta(SqlConnection con, SqlTransaction Transaccion,double Total,DateTime Fecha,
            double ImporteEfectivo,double ImporteTarjeta,Int32? CodTarjeta,Int32? CodCorte)
        {
            string sql = " insert into Venta(Total,Fecha,ImporteEfectivo,ImporteTarjeta,CodTarjeta,CodCorte)";
            sql = sql + " values (" + Total.ToString().Replace(",", ".");
            sql = sql + "," + "'" + Fecha.ToShortDateString() + "'";
            sql = sql + "," + ImporteEfectivo.ToString().Replace(",", ".");
            sql = sql + "," + ImporteTarjeta.ToString().Replace(",", ".");
            if (CodTarjeta != null)
                sql = sql + "," + CodTarjeta.ToString();
            else
                sql = sql + ",null";
            if (CodCorte != null)
                sql = sql + "," + CodCorte.ToString();
            else
                sql = sql + ",null ";
            sql = sql + ")";
            return cDb.EjecutarEscalarTransaccion(con, Transaccion, sql);
        }

        public void InsertarDetalleVenta(SqlConnection con, SqlTransaction Transaccion,Int32 CodVenta, double Cantidad, Double Precio,
            Int32 CodProducto, double  Subtotal)
        {
            string sql = " insert into DetalleVenta(CodVenta,CodProducto,Cantidad,Precio,Subtotal)";
            sql = sql + " values (" + CodVenta.ToString();
            sql = sql + "," + CodProducto.ToString();
            sql = sql + "," + Cantidad.ToString().Replace(",", ".");
            sql = sql + "," + Precio.ToString().Replace(",", ".");
            sql = sql + "," + Subtotal.ToString().Replace(",", ".");
            sql = sql + ")";
             cDb.EjecutarNonQueryTransaccion (con, Transaccion, sql);
        }

        public DataTable GetVentasxFecha(DateTime FechaDesde, DateTime FechaHasta)
        {
            string sql = "select CodVenta,Fecha,ImporteEfectivo,ImporteTarjeta, Total";
            sql = sql + " from Venta ";  
            sql = sql + " where Fecha>=" + "'" + FechaDesde.ToShortDateString () + "'";
            sql = sql + " and Fecha <=" + "'" + FechaHasta.ToShortDateString() + "'";
            return cDb.ExecuteDataTable(sql);
        }

        public DataTable GetVentaxCodigo(Int32 CodVenta)
        {
            string sql = " select *";
            sql = sql + " from Venta v,DetalleVenta d,Producto p";
            sql = sql + " where v.CodVenta = d.CodVenta";
            sql = sql + " and d.CodProducto=p.CodProducto";
            sql = sql + " and v.CodVenta =" + CodVenta.ToString ();
            return cDb.ExecuteDataTable(sql);
        }

        public void BorrarVenta(SqlConnection con, SqlTransaction Transaccion,Int32 CodVenta)
        {
            
           string sql = "delete from venta where CodVenta=" + CodVenta.ToString ();
            cDb.EjecutarNonQueryTransaccion(con, Transaccion, sql);
            sql = "delete from DetalleVenta where CodVenta=" + CodVenta.ToString ();
            cDb.EjecutarNonQueryTransaccion(con, Transaccion, sql);
            sql = "delete from movimiento where CodVenta=" + CodVenta.ToString ();
            cDb.EjecutarNonQueryTransaccion(con, Transaccion, sql);
        }

        public DataTable GetVentasxCodCorte(Int32 CodCorte)
        {
            string sql = "select * from venta where CodCorte=" + CodCorte.ToString();
            return cDb.ExecuteDataTable(sql);
        }

        public double Balance(DateTime fecha)
        {
            double Total = 0;
            string sql = "select sum(Total) as Total ";
            sql = sql + " from Venta ";
            sql = sql + " where Fecha=" + "'" + fecha.ToShortDateString() + "'";
            DataTable trdo = cDb.ExecuteDataTable(sql);
            if (trdo.Rows.Count > 0)
                if (trdo.Rows[0]["Total"].ToString() != "")
                    Total = Convert.ToDouble(trdo.Rows[0]["Total"].ToString());
            return Total;
        }
    }
}

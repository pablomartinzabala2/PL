using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace PELUQUERIA.Clases
{
    public  class cMovimiento
    {
        public void  InsertarMovimientoTransaccion(SqlConnection con, SqlTransaction Transaccion, string Descripcion, DateTime Fecha,
            double ImporteEfectivo,double ImporteTarjeta,Int32? CodTarjeta,
            Int32? CodCorte, Int32? CodGasto, double ImporteGasto,Int32? CodPago, double Debe, Double Haber,Int32? CodVenta)
        {
            string sql = "insert into Movimiento(Descripcion,Fecha";
            sql = sql + ",ImporteEfectivo, ImporteTarjeta, CodTarjeta, CodCorte,CodGasto,ImporteGasto,CodPago,Debe,Haber,CodVenta)";
            sql = sql + " values (" + "'" + Descripcion + "'";
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
                sql = sql + ",null";
            
            if (CodGasto != null)
                sql = sql + "," + CodGasto.ToString();
            else  
                sql = sql + ",null";
            sql = sql + "," + ImporteGasto.ToString().Replace(",", ".");
            if (CodPago != null)
                sql = sql + "," + CodPago.ToString();
            else
                sql = sql + ",null";
            sql = sql + "," + Debe.ToString().Replace(",", ".");
            sql = sql + "," + Haber.ToString().Replace(",", ".");
            if (CodVenta != null)
                sql = sql + "," + CodVenta.ToString();
            else
                sql = sql + ",null";

            sql = sql + ")";

             cDb.EjecutarNonQueryTransaccion (con, Transaccion, sql);
        }

        public double  GetImporteEfectivos(DateTime FechaDesde, DateTime FechaHasta, Boolean Procesado)
        {
            double Importe =0;
            string sql = "select sum(ImporteEfectivo) as Importe from Movimiento";
            sql = sql + " where Fecha>= " + "'" + FechaDesde.ToShortDateString () + "'";
            sql = sql + " and Fecha<=" + "'" + FechaHasta.ToShortDateString() + "'";
            sql = sql + " and CodVenta is null";
            if (Procesado == true)
                sql = sql + " and Procesado is null";
            DataTable trdo = cDb.ExecuteDataTable(sql);
            if (trdo.Rows.Count > 0)
                if (trdo.Rows[0]["Importe"].ToString() != "")
                    Importe = Convert.ToDouble(trdo.Rows[0]["Importe"].ToString());
            return Importe;
 
        }

        public double GetImporteTarjeta(DateTime FechaDesde, DateTime FechaHasta, Boolean Procesado)
        {
            double Importe = 0;
            string sql = "select sum(ImporteTarjeta) as Importe from Movimiento";
            sql = sql + " where Fecha>= " + "'" + FechaDesde.ToShortDateString() + "'";
            sql = sql + " and Fecha<=" + "'" + FechaHasta.ToShortDateString() + "'";
            sql = sql + " and CodVenta is null";
            if (Procesado == true)
                sql = sql + " and Procesado is null";
            DataTable trdo = cDb.ExecuteDataTable(sql);
            if (trdo.Rows.Count > 0)
                if (trdo.Rows[0]["Importe"].ToString() != "")
                    Importe = Convert.ToDouble(trdo.Rows[0]["Importe"].ToString());
            return Importe;

        }

        public double GetImporteEfectivosVenta(DateTime FechaDesde, DateTime FechaHasta, Boolean Procesado)
        {
            double Importe = 0;
            string sql = "select sum(ImporteEfectivo) as Importe from Movimiento";
            sql = sql + " where Fecha>= " + "'" + FechaDesde.ToShortDateString() + "'";
            sql = sql + " and Fecha<=" + "'" + FechaHasta.ToShortDateString() + "'";
            sql = sql + " and CodCorte is null";
            if (Procesado == true)
                sql = sql + " and Procesado is null ";
            DataTable trdo = cDb.ExecuteDataTable(sql);
            if (trdo.Rows.Count > 0)
                if (trdo.Rows[0]["Importe"].ToString() != "")
                    Importe = Convert.ToDouble(trdo.Rows[0]["Importe"].ToString());
            return Importe;

        }

        public double GetImporteTarjetaVenta(DateTime FechaDesde, DateTime FechaHasta,Boolean Procesado)
        {
            double Importe = 0;
            string sql = "select sum(ImporteTarjeta) as Importe from Movimiento";
            sql = sql + " where Fecha>= " + "'" + FechaDesde.ToShortDateString() + "'";
            sql = sql + " and Fecha<=" + "'" + FechaHasta.ToShortDateString() + "'";
            sql = sql + " and CodCorte is null";
            if (Procesado == true)
                sql = sql + " and Procesado is null";
            DataTable trdo = cDb.ExecuteDataTable(sql);
            if (trdo.Rows.Count > 0)
                if (trdo.Rows[0]["Importe"].ToString() != "")
                    Importe = Convert.ToDouble(trdo.Rows[0]["Importe"].ToString());
            return Importe;

        }

        public double GetImporteGasto(DateTime FechaDesde, DateTime FechaHasta,Boolean Procesado,Int32? CodTipoGasto)
        {
            double Importe = 0;
            string sql = "select sum(ImporteGasto) as Importe from Movimiento m,Gasto g";
            sql = sql + " where m.CodGasto = g.CodGasto";
            sql = sql + " and m.Fecha>= " + "'" + FechaDesde.ToShortDateString() + "'";
            sql = sql + " and m.Fecha<=" + "'" + FechaHasta.ToShortDateString() + "'";
            if (Procesado == true)
                sql = sql + " and Procesado is null";
            if (CodTipoGasto != null)
                sql = sql + " and g.CodTipoGasto=" + CodTipoGasto.ToString ();
            DataTable trdo = cDb.ExecuteDataTable(sql);
            if (trdo.Rows.Count > 0)
                if (trdo.Rows[0]["Importe"].ToString() != "")
                    Importe = Convert.ToDouble(trdo.Rows[0]["Importe"].ToString());
            return Importe;

        }

        public DataTable GetMovimientoxCodCorte(Int32 CodCorte)
        {
            string sql = " select * from Movimiento";
            sql = sql + " where CodCorte =" + CodCorte.ToString ();
            return cDb.ExecuteDataTable(sql);
        }

        public void ActualizarProceso()
        {
            string sql = "update movimiento set Procesado =1";
            cDb.ExecutarNonQuery(sql);
        }

        public void BorrarMovimientoxCodGasto(SqlConnection con, SqlTransaction Transaccion, Int32 CodGasto)
        {
            string sql = "delete from Movimiento where CodGasto=" + CodGasto.ToString();
            cDb.EjecutarNonQueryTransaccion(con, Transaccion, sql);
        }

        public double BalanceTarjeta(DateTime fecha)
        {
            double Total = 0;
            string sql = "select sum(ImporteTarjeta) as Total ";
            sql = sql + " from Movimiento ";
            sql = sql + " where Fecha=" + "'" + fecha.ToShortDateString() + "'";
            DataTable trdo = cDb.ExecuteDataTable(sql);
            if (trdo.Rows.Count > 0)
                if (trdo.Rows[0]["Total"].ToString() != "")
                    Total = Convert.ToDouble(trdo.Rows[0]["Total"].ToString());
            return Total;
        }

        public void BorrarMovimientoxCodCorte(SqlConnection con, SqlTransaction Transaccion, Int32 CodCorte)
        {
            string sql = "delete from Movimiento where CodCorte=" + CodCorte.ToString();
            cDb.EjecutarNonQueryTransaccion(con, Transaccion, sql);
        }
    }
}

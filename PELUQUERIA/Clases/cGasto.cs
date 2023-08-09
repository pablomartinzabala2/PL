using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace PELUQUERIA.Clases
{
   public  class cGasto
    {
       public Int32 InsertarGastoTransaccion(SqlConnection con, SqlTransaction Transaccion, string Descripcion, DateTime Fecha, double Total,Int32? CodTipoGasto,Int32? CodProveedor)
       {
           string sql = "insert into Gasto(Descripcion,Fecha,Total,CodTipoGasto,CodProveedor)";
           sql = sql + " values (" + "'" + Descripcion + "'";
           sql = sql + "," + "'" + Fecha.ToShortDateString() + "'";
           sql = sql + "," + Total.ToString().Replace(",", ".");
           if (CodTipoGasto != null)
               sql = sql + "," + CodTipoGasto.ToString();
           else
               sql = sql + ",null";

           if (CodProveedor != null)
               sql = sql + "," + CodProveedor.ToString();
           else
               sql = sql + ",null";

           sql = sql + ")";
           return  cDb.EjecutarEscalarTransaccion (con, Transaccion, sql);
       }

       public DataTable GetGastos(DateTime FechaDesde, DateTime FechaHasta,string Descripcion,
           int TipoGastoParticular,int TipoGastoGeneral,int Proveedor, Int32? CodProveedor , int TipoGastoRetiro
           )
       {
           string sql = "select g.CodGasto,g.Descripcion,g.Fecha,g.Total,g.CodProveedor";
           sql = sql + ",(select pr.Nombre from Proveedor Pr where Pr.CodProveedor =g.CodProveedor) as Proveedor";
           sql = sql + " from Gasto g";
           sql = sql + " where g.Fecha>=" + "'" + FechaDesde.ToShortDateString () + "'" ;
           sql = sql + " and g.Fecha<=" + "'" + FechaHasta.ToShortDateString() + "'";
           if ((TipoGastoParticular + TipoGastoGeneral + Proveedor + TipoGastoRetiro)  > 0)
           {
               sql = sql + " and g.CodTipoGasto in(" + TipoGastoParticular.ToString();
               sql = sql + "," + TipoGastoGeneral.ToString();
               sql = sql + "," + Proveedor.ToString();
               sql = sql + "," + TipoGastoRetiro.ToString(); 
               sql = sql + ")";
 
           }
                     /*
           if (TipoGastoGeneral ==1)
               sql = sql + " and g.CodTipoGasto=1";
           if (TipoGastoParticular ==1)
               sql = sql + " and g.CodTipoGasto=2";
           if (Proveedor == 1)
           {
               sql = sql + " and CodTipoGasto=3";
               if (CodProveedor != null)
                   sql = sql + " and g.CodProveedor=" + CodProveedor.ToString ();
           }
             */  
           if (Descripcion != "")
               sql = sql + " and Descripcion like " + "'%" + Descripcion + "%'" ;
           if (Proveedor >0)
           if (CodProveedor != null)
               sql = sql + " and g.CodProveedor=" + CodProveedor.ToString(); 
           sql = sql + " order by CodGasto Desc";
           return cDb.ExecuteDataTable(sql);
       }

       public void BorrarGasto(SqlConnection con, SqlTransaction Transaccion, Int32 CodGasto)
       {
           string sql = "delete from Gasto where CodGasto=" + CodGasto.ToString ();
           cDb.EjecutarNonQueryTransaccion(con, Transaccion, sql);
       }

       public double GetTotalxCodTipoGasto(DateTime FechaDesde, DateTime FechaHasta, int? CodTipoGasto, Int32? CodProveedor)
       {
           double Importe = 0;
           string sql = "select sum(Total) as Total";
           sql = sql + " from Gasto";
           sql = sql + " where Fecha>=" + "'" + FechaDesde.ToShortDateString() + "'";
           sql = sql + " and Fecha<=" + "'" + FechaHasta.ToShortDateString() + "'";
           if (CodTipoGasto != null)
               sql = sql + " and CodTipoGasto=" + CodTipoGasto.ToString();
           if (CodProveedor != null)
               sql = sql + " and CodProveedor=" + CodProveedor.ToString();
           DataTable trdo = cDb.ExecuteDataTable(sql);
           if (trdo.Rows.Count > 0)
               if (trdo.Rows[0]["Total"].ToString() != "")
                   Importe = Convert.ToDouble(trdo.Rows[0]["Total"].ToString());
           return Importe;
       }

       public double Balance(DateTime fecha,int CodTipoGasto)
       {
           double Total = 0;
           string sql = "select sum(Total) as Total ";
           sql = sql + " from Gasto ";
           sql = sql + " where Fecha=" + "'" + fecha.ToShortDateString() + "'";
           sql = sql + " and CodTipoGasto=" + CodTipoGasto.ToString() ;
           DataTable trdo = cDb.ExecuteDataTable(sql);
           if (trdo.Rows.Count > 0)
               if (trdo.Rows[0]["Total"].ToString() != "")
                   Total = Convert.ToDouble(trdo.Rows[0]["Total"].ToString());
           return Total;
       }
   }
}

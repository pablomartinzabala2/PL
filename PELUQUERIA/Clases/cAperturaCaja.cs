using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data ;
namespace PELUQUERIA.Clases
{
    public class cAperturaCaja
    {
        public void AbrirCaja(double Importe,DateTime Fecha)
        {
            string sql =" insert into AperturaCaja(Importe,FechaAbierta)";
            sql = sql + " values(" + Importe.ToString ().Replace (",",".");
            sql = sql + "," + "'" + Fecha.ToShortDateString () + "'";
            sql = sql + ")";
            cDb.ExecutarNonQuery (sql);
        }

        public void CierreCaja(Int32 CodApertura,DateTime Fecha)
        {
            string sql =" update AperturaCaja";
            sql = sql + " set FechaCerrada=" + "'" + Fecha.ToShortDateString () + "'";
            sql = sql + " where CodApertura =" + CodApertura.ToString ();
            cDb.ExecutarNonQuery(sql);
        }

        public DataTable GetUltimaApertura()
        {
            string sql = "select * ";
            sql = sql + " from AperturaCaja";
            sql = sql + " where FechaCerrada is null";
            return cDb.ExecuteDataTable(sql);
        }

        public double GetUltimoImporte(DateTime Fecha)
        {
            Int32 CodApertura =0;
            string sql = " select max(CodApertura) as CodApertura";
            sql = sql + " from AperturaCaja";
            //sql = sql + " where FechaAbierta=" + "'" + Fecha.ToShortDateString() + "'";
            sql = sql + " where FechaCerrada is null";
            DataTable trdo = cDb.ExecuteDataTable(sql);
            if (trdo.Rows.Count > 0)
            {
                if (trdo.Rows[0]["CodApertura"].ToString() != "")
                {
                     CodApertura = Convert.ToInt32(trdo.Rows[0]["CodApertura"].ToString());
                }
            }
            double Total =0;
            if (CodApertura > 0)
            {
                sql = "select Importe from aperturacaja";
                sql = sql + " where CodApertura=" + CodApertura.ToString ();
                DataTable tresul = cDb.ExecuteDataTable(sql);
                if (tresul.Rows.Count > 0)
                    if (tresul.Rows[0]["Importe"].ToString() != "")
                        Total = Convert.ToDouble(tresul.Rows[0]["Importe"].ToString());
            }
            return Total;
        }

        public Boolean EstaAbierta()
        {
            Boolean Abierta = false;
            string sql = "select * from AperturaCaja";
            sql = sql + " where FechaCerrada is null";
            DataTable trdo = cDb.ExecuteDataTable(sql);
            if (trdo.Rows.Count >0)
                if (trdo.Rows[0]["FechaAbierta"].ToString() != "")
                {
                    Abierta = true;
                }
            return Abierta;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace PELUQUERIA.Clases
{
    public class cDetalleCorte
    {
        public string GetPeluquerosxCodCorte(Int32 CodCorte)
        {
            string Pel = "";
            DataTable tb = new DataTable();
            tb.Columns.Add("CodPeluquero");
            string sql = " select p.CodPeluquero,p.Nombre,p.Apellido";
            sql = sql + " FROM DETALLECORTE dc,PELUQUERO p";
            sql = sql + " where dc.CodPeluquero=p.CodPeluquero";
            sql = sql + " and dc.CodCorte=" + CodCorte.ToString();
            DataTable trdo = cDb.ExecuteDataTable(sql);
            for (int i = 0; i < trdo.Rows.Count; i++)
            {
                string Nom = trdo.Rows[i]["Nombre"].ToString();
                string Ape = trdo.Rows[i]["Apellido"].ToString();
                string CodPeluquero = trdo.Rows[i]["CodPeluquero"].ToString();
                if (Pel == "")
                {
                    Pel = Nom + " " + Ape;
                    DataRow r = tb.NewRow();
                    r["CodPeluquero"] = CodPeluquero.ToString();
                    tb.Rows.Add(r);
                    tb.AcceptChanges();
                }
                else
                {
                    int b =0;
                    for (int k = 0; k < tb.Rows.Count; k++)
                    {
                        if (tb.Rows[k]["CodPeluquero"].ToString() == CodPeluquero)
                            b = 1;
                    }
                    if (b == 0)
                    {
                        DataRow r = tb.NewRow();
                        r["CodPeluquero"] = CodPeluquero.ToString();
                        tb.Rows.Add(r);
                        tb.AcceptChanges();
                        Pel = Pel + ", " + Nom + " " + Ape;
                    }
                        
                }
            }
            return Pel;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace PELUQUERIA.Clases
{
    public  class cReporte
    {
        public void Borrar()
        {
            string sql = "delete from Reporte";
            cDb.ExecutarNonQuery(sql);
        }

        public void Insertar(string Campo1, string Campo2, string Campo3,
            string Campo4, string Campo5, string Campo6, string Campo7,string Campo8)
        {
            string sql = "insert into Reporte(Campo1,Campo2,Campo3";
            sql = sql + ",Campo4,Campo5,Campo6,Campo7,Campo8)";
            sql = sql + "Values(" + "'" + Campo1 + "'";
            sql = sql + "," + "'" + Campo2 + "'";
            sql = sql + "," + "'" + Campo3 + "'";
            sql = sql + "," + "'" + Campo4 + "'";
            sql = sql + "," + "'" + Campo5 + "'";
            sql = sql + "," + "'" + Campo6 + "'";
            sql = sql + "," + "'" + Campo7 + "'";
            sql = sql + "," + "'" + Campo8 + "'";
            sql = sql + ")";
            cDb.ExecutarNonQuery(sql);
        }
    }
}

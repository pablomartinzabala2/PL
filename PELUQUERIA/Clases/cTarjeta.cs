using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace PELUQUERIA.Clases
{
    public class cTarjeta
    {
        public DataTable GetTarjetasActivos()
        {
            string sql = " select * from Tarjeta ";
            sql = sql + " where Inactivo is null or Inactivo =0";
            sql = sql + " order by nombre";
            return cDb.ExecuteDataTable(sql);
        }

        public DataTable GetTrabajosxTarjeta(DateTime FechaDesde, DateTime FechaHasta,Int32? CodTarjeta)
        {
            string sql = " select t.Nombre,m.Fecha,cli.Nombre,cli.Apellido, m.ImporteTarjeta";
            sql = sql + " from Movimiento m,tarjeta t";
            sql = sql + ",Corte c,Cliente cli ";
            sql = sql + " where m.CodTarjeta=t.CodTarjeta";
            sql = sql + " and m.CodCorte = c.CodCorte";
            sql = sql + " and c.CodCliente= cli.CodCliente";
            sql = sql + " and m.CodTarjeta is not null";
            sql = sql + " and m.Fecha>=" + "'" + FechaDesde.ToShortDateString() + "'";
            sql = sql + " and m.Fecha<=" + "'" + FechaHasta.ToShortDateString() + "'";
            if (CodTarjeta != null)
                sql = sql + " and m.CodTarjeta=" + CodTarjeta.ToString () ;
            return cDb.ExecuteDataTable(sql);
        }
    }
}

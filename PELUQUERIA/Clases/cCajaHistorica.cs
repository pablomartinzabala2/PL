using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace PELUQUERIA.Clases
{
    class cCajaHistorica
    {
        public void InsertarCajaHistorica(Int32 CodApertura,double EfectivoPr,double TarjetaPr,
            double EfectivoVentas,double TarjetaVentas,
            double GastosGenerales,double GastosParticulares,double Proveedores ,
            double MontoInicial,double Retiro
            )
        {
            double TotalPr = EfectivoPr + TarjetaPr;
            double TotalVentas = EfectivoVentas + TarjetaVentas;
            string sql = " delete from CajaHistorica where CodApertura =" + CodApertura.ToString();
            cDb.ExecutarNonQuery(sql);
            sql = " insert into CajaHistorica(CodApertura,TotalPr,EfectivoPr";
            sql = sql + ",TarjetaPr,TotalVentas,EfectivoVentas,TarjetaVentas,";
            sql = sql + "GastosGenerales,GastosParticulares,Proveedores,MontoInicial,Retiro)";
            sql = sql + " values (" + CodApertura.ToString();
            sql = sql + "," + TotalPr.ToString().Replace(",", ".");
            sql = sql + "," + EfectivoPr.ToString().Replace(",", ".");
            sql = sql + "," + TarjetaPr.ToString().Replace(",", ".");
            sql = sql + "," + TotalVentas.ToString().Replace(",", ".");
            sql = sql + "," + EfectivoVentas.ToString().Replace(",", ".");
            sql = sql + "," + TarjetaVentas.ToString().Replace(",", ".");
            sql = sql + "," + GastosGenerales.ToString().Replace(",", ".");
            sql = sql + "," + GastosParticulares.ToString().Replace(",", ".");
            sql = sql + "," + Proveedores.ToString().Replace(",", ".");
            sql = sql + "," + MontoInicial.ToString().Replace(",", ".");
            sql = sql + "," + Retiro.ToString().Replace(",", ".");
            sql = sql + ")";
            cDb.ExecutarNonQuery(sql);
        }

        public DataTable GetCajaHistorica(DateTime FechaDesde, DateTime FechaHasta)
        {
            string sql = " select a.CodApertura,h.MontoInicial,a.FechaAbierta as Fecha";
            sql = sql + " from AperturaCaja a,CajaHistorica h";
            sql = sql + " where a.CodApertura=h.CodApertura";
            sql = sql + " and a.FechaAbierta>=" + "'" + FechaDesde.ToShortDateString () + "'";
            sql = sql + " and a.FechaAbierta <=" +"'" + FechaHasta.ToShortDateString() + "'";
            sql = sql + " order by a.CodApertura desc";
            return cDb.ExecuteDataTable(sql);
        }

        public DataTable GetMontosCajaHistorica(Int32 CodApertura)
        {
            string sql = "select * from CajaHistorica";
            sql = sql + " where CodApertura=" + CodApertura.ToString ();
            return cDb.ExecuteDataTable(sql);
        }
    }
}

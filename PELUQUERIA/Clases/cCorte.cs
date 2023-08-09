using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace PELUQUERIA.Clases
{
    public class cCorte
    {
        public Int32 InsertarCorteTransaccion(SqlConnection con, SqlTransaction Transaccion, DateTime Fecha,Int32 CodCliente,
             string Detalle,double Total)
        {
            string sql = " insert into Corte(Fecha,CodCliente,Total,Detalle)";
            sql = sql + "Values(" + "'" + Fecha.ToShortDateString() + "'";
            sql = sql + "," + CodCliente.ToString();
            sql = sql + "," + Total.ToString().Replace(",", ".");
            sql = sql + "," + "'" + Detalle + "'";
            sql = sql + ")";
            return cDb.EjecutarEscalarTransaccion(con, Transaccion, sql);
        }

        public void InsertarDetalleCorte(SqlConnection con, SqlTransaction Transaccion,Int32 CodCorte,Int32 CodTrabajo, double SubTotal,Int32 CodPeluquero)
        {
            string sql = "Insert into DetalleCorte(CodCorte,CodTrabajo,SubTotal,CodPeluquero)";
            sql = sql + " values (" + CodCorte;
            sql = sql + "," + CodTrabajo;
            sql = sql + "," + SubTotal.ToString().Replace(",", ".");
            sql = sql + "," + CodPeluquero.ToString(); 
            sql = sql + ")";
            cDb.EjecutarNonQueryTransaccion(con, Transaccion, sql);
        }

        public DataTable GetCortes(DateTime? FechaDesde, DateTime? FechaHasta, string NombreCliente,
            string ApellidoCliente, Int32? CodPeluquero,Int32? NroDocumento,Int32? CodCliente)
        {
            string sql = " select Cor.CodCorte, cli.Apellido,cli.Nombre,cli.Telefono,cor.Fecha,cor.Total";
            
            sql = sql + " from Cliente cli,Corte Cor";
            sql = sql + " where Cor.CodCliente = cli.CodCliente";
            
            if (ApellidoCliente != "")
            {
                sql = sql + " and Cli.Apellido like " + "'%" + ApellidoCliente + "%'";
            }

            if (NombreCliente  != "")
            {
                sql = sql + " and Cli.Nombre like " + "'%" + NombreCliente  + "%'";
            }

            if (CodPeluquero != null)
            {
                sql = sql + " and Cor.CodPeluquero=" + CodPeluquero.ToString();
            }
            if (NroDocumento != null)
            {
                sql = sql + " and Cli.NroDocumento=" + NroDocumento.ToString();
            }
            if (FechaDesde != null)
            {

                DateTime fec = Convert.ToDateTime(FechaDesde);
                DateTime fecHasta = Convert.ToDateTime(FechaHasta); 
                sql = sql + " and Cor.Fecha >=" + "'" + fec.ToShortDateString () + "'";
                sql = sql + " and Cor.Fecha <=" + "'" + fecHasta.ToShortDateString () + "'";
            }
            if (CodCliente != null)
                sql = sql + " and Cli.CodCliente=" + CodCliente.ToString();
            sql = sql + " order by Cor.Fecha Desc, cli.Apellido,cli.Nombre";

            return cDb.ExecuteDataTable (sql);
        }

        public DataTable GetCortexCodigo(Int32 CodCorte)
        {
            string sql = "select * from Corte where CodCorte =" + CodCorte.ToString();
            return cDb.ExecuteDataTable(sql);
        }

        public DataTable BuscarDetalleCorte(Int32 CodCorte)
        {
            string sql = "select Tra.CodTrabajo,Tra.Nombre,pel.CodPeluquero,(Pel.Apellido + ' ' + Pel.Nombre) as Peluquero,Det.Subtotal";
            sql = sql + " from Trabajo Tra,DetalleCorte Det,Peluquero Pel";
            sql = sql + " where Det.CodTrabajo =Tra.CodTrabajo ";
            sql = sql + " and Det.CodCorte=" + CodCorte.ToString();
            sql = sql + " and Det.CodPeluquero=Pel.CodPeluquero";
            return cDb.ExecuteDataTable(sql);
        }

        public DataTable GetCortesResumen(DateTime? FechaDesde, DateTime? FechaHasta,Int32? CodPeluquero)
        {
            string sql = "select Pel.CodPeluquero,Pel.Nombre,Pel.Apellido,Pel.Porcentaje,Count(d.CodCorte) as Cantidad,Sum(d.SubTotal) as Total";
            sql = sql + " from Peluquero Pel, DetalleCorte d,Corte c";
            sql = sql + " where c.CodCorte = d.CodCorte";
            sql = sql + " and Pel.CodPeluquero = d.CodPeluquero";
            if (CodPeluquero != null)
                sql = sql + " and Pel.CodPeluquero =" + CodPeluquero.ToString ();
            if (FechaDesde != null)
                sql = sql + " and c.Fecha>=" + "'" + FechaDesde.ToString() + "'";
            if (FechaHasta != null)
                sql = sql + " and c.Fecha<=" + "'" + FechaHasta.ToString() + "'";

            sql = sql + " group by Pel.CodPeluquero,Pel.Nombre,Pel.Apellido,Pel.Porcentaje";
            return cDb.ExecuteDataTable(sql);
        }

        public DataTable GetCortesResumenxTarea(DateTime? FechaDesde, DateTime? FechaHasta,Int32? CodPeluquero)
        {
            string sql = "select t.nombre as Tarea,count(d.SubTotal) as Cantidad, sum(d.SubTotal) as Total";
            sql = sql + " from DetalleCorte d,Trabajo t,Corte c";
            sql = sql + " where d.CodTrabajo = t.CodTrabajo";
            sql = sql + " and d.CodCorte = c.CodCorte";
            if (FechaDesde != null)
            {
                DateTime f1 = Convert.ToDateTime(FechaDesde);
                DateTime f2 = Convert.ToDateTime(FechaHasta);
                sql = sql + " and c.Fecha>=" +"'" + f1.ToShortDateString () + "'" ;
                sql = sql + " and c.Fecha <=" + "'" + f2.ToShortDateString() + "'"; 

            }
            if (CodPeluquero != null)
                sql = sql + " and d.CodPeluquero=" + CodPeluquero.ToString ();
            sql = sql + " group by t.nombre";
            sql = sql + " having count(d.SubTotal)>0";
            sql = sql + " order by count(d.SubTotal) desc";
            return cDb.ExecuteDataTable(sql);
        }

        public void BorrarCorte(SqlConnection con, SqlTransaction Transaccion, Int32 CodCorte)
        {
            string sql = "delete from corte where CodCorte =" + CodCorte.ToString();
            cDb.EjecutarNonQueryTransaccion(con, Transaccion, sql);
        }

        public void BorrarDetalleCorte(SqlConnection con, SqlTransaction Transaccion, Int32 CodCorte)
        {
            string sql = "delete from DetalleCorte where CodCorte =" + CodCorte.ToString();
            cDb.EjecutarNonQueryTransaccion(con, Transaccion, sql);
        }

        public void BorrarCortexCliente(Int32 CodCliente)
        {
            string sql = "delete from corte where CodCliente =" + CodCliente.ToString();
            cDb.ExecutarNonQuery(sql);
        }

        public DataTable GetTareasDiarias(DateTime FechaDesde, DateTime FechaHasta,Int32? CodPeluquero)
        {
            string sql = " select c.CodCorte,c.Fecha,(p.Nombre + ' ' + p.Apellido) as Empleado,t.Nombre as Tarea,(cli.Nombre + ' ' + cli.Apellido) as Cliente,d.SubTotal as Importe";
            sql = sql + " from Corte c,DetalleCorte d,Trabajo t,Peluquero p,Cliente cli";
            sql = sql + " where c.CodCorte=d.CodCorte";
            sql = sql + " and t.CodTrabajo=d.CodTrabajo";
            sql = sql + " and d.CodPeluquero=p.CodPeluquero";
            sql = sql + " and c.CodCliente= cli.CodCliente";
            sql = sql + " and c.Fecha>=" + "'" + FechaDesde.ToShortDateString () + "'";
            sql = sql + " and c.Fecha <=" + "'" + FechaHasta.ToShortDateString() + "'";
            if (CodPeluquero != null)
                sql = sql + " and d.CodPeluquero=" + CodPeluquero.ToString ();
            return cDb.ExecuteDataTable(sql);
        }

        public DataTable GetCortesResumidoxDia(DateTime FechaDesde, DateTime FechaHasta,Int32? CodPeluquero)
        {
            string sql = "select fecha,p.Apellido,p.Nombre, sum(d.SubTotal) as Total,p.Porcentaje";
            sql = sql + " from corte c,peluquero p,DetalleCorte d";
            sql = sql + " where d.CodPeluquero = p.CodPeluquero";
            sql = sql + " and c.CodCorte= d.CodCorte";
            sql = sql + " and c.fecha >=" + "'" + FechaDesde.ToShortDateString() + "'";
            sql = sql + " and c.fecha <=" + "'" + FechaHasta.ToShortDateString() + "'";
            if (CodPeluquero !=null)
                sql = sql + " and d.CodPeluquero=" + CodPeluquero.ToString ();
            sql = sql + " group by c.fecha, p.Apellido,p.Nombre,p.Porcentaje";
            return cDb.ExecuteDataTable(sql);
        }
        
        public double Balance(DateTime fecha)
        {
            double Total =0;
            string sql = "select sum(Total) as Total ";
            sql = sql + " from Corte ";
            sql = sql + " where Fecha=" + "'" + fecha.ToShortDateString () + "'";
            DataTable trdo = cDb.ExecuteDataTable (sql);
            if (trdo.Rows.Count >0)
                if (trdo.Rows[0]["Total"].ToString ()!="") 
                    Total = Convert.ToDouble (trdo.Rows[0]["Total"].ToString ());
            return Total;
        }
        /*
        public double GetTotalTarjeta(DateTime Fecha)
        {
            string sql = "select sum(ImporteTarjeta) as Total";
            sql = sql + " where CodCorte is not null";
        }
         * */
    }
}

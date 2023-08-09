using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace PELUQUERIA.Clases
{
    public class cCliente
    {
        public DataTable GetClientexCodigo(Int32 CodCliente)
        {
            string sql = "select * from cliente where CodCliente =" + CodCliente.ToString();
            return cDb.ExecuteDataTable(sql); 
        }

        public Int32 InsertarClienteTran(SqlConnection con, SqlTransaction Transaccion, string Apellido, string Nombre,string Email,string Telefono,int? Dia,int? Mes, int? CodTipoDoc,string NroDocumento)
        {
            string sql = "Insert into Cliente";
            sql = sql + "(Nombre,Apellido,Telefono,Email,Dia,Mes,CodTipoDoc,NroDocumento)";
            sql = sql + " values (";
            sql = sql + "'" + Nombre + "'";
            sql = sql + "," + "'" + Apellido + "'";
            sql = sql + "," + "'" + Telefono + "'";
            sql = sql + "," + "'" + Email + "'";
            if (Dia != null)
                sql = sql + "," + Dia.ToString();
            else
                sql = sql + ",null";
            if (Mes != null)
                sql = sql + "," + Mes.ToString();
            else
                sql = sql + ",null";
            if (CodTipoDoc != null)
                sql = sql + "," + CodTipoDoc.ToString();
            else
                sql = sql + ",null";
            sql = sql + "," + "'" + NroDocumento +"'";
            sql = sql + ")";
            return cDb.EjecutarEscalarTransaccion(con, Transaccion, sql);
        }

        public void ModificarClienteTran(SqlConnection con, SqlTransaction Transaccion,Int32 CodCliente, string Apellido, string Nombre, string Email, string Telefono, int? Dia, int? Mes,int? CodTipoDoc,string NroDocumento)
        {
            string sql = "Update Cliente ";
            sql = sql + "set Nombre =" + "'" + Nombre + "'";
            sql = sql + " ,Apellido =" + "'" + Apellido + "'";
            sql = sql + ", Telefono =" + "'" + Telefono + "'";
             sql = sql + ", Email =" + "'" + Email + "'";
             if (Dia != null)
                 sql = sql + ",Dia=" + Dia.ToString();
             else
                 sql = sql + ",Dia =null";
            if (Mes !=null)
                sql = sql + ",Mes =" + Mes.ToString ();
            else
                sql = sql + ",Mes =null";
            if (CodTipoDoc  != null)
                sql = sql + ",CodTipoDoc=" + CodTipoDoc.ToString();
            else
                sql = sql + ",CodTipoDoc =null";
            sql = sql + ",NroDocumento=" + "'" + NroDocumento + "'";
            sql = sql + " where CodCliente =" + CodCliente.ToString();
            

            cDb.EjecutarNonQueryTransaccion(con, Transaccion, sql);
            
        }

        public DataTable GetCumplenios(int Dia, int Mes)
        {
            string sql = "select * from cliente ";
            sql = sql + " where Dia =" + Dia.ToString();
            sql = sql + " and Mes=" + Mes.ToString ();
            return cDb.ExecuteDataTable(sql);
        }

        public DataTable GetClientesxNroDocumento(string NroDocumento)
        {
            string sql = "select * from cliente where NroDocumento =" + "'" + NroDocumento + "'";
            return cDb.ExecuteDataTable(sql);
        }

        public DataTable GetClientexNomApe(string Nombre, String Apellido)
        {
            string sql = "select * from cliente";
            sql = sql + " where Nombre like " + "'%" + Nombre + "%'";
            sql = sql + " and apellido like " + "'%" + Apellido + "%'";
            return cDb.ExecuteDataTable(sql);
        }

        public void BorrarCliente(Int32 CodCliente)
        {
            string sql = "Delete from cliente where CodCliente=" + CodCliente.ToString();
            cDb.ExecutarNonQuery (sql);
        }

        public Int32 GetCodClienteNulo()
        {
            Int32 CodigoCliente = -1;
            int ban = 0;
            string sql = "select CodCliente";
            sql = sql + " from Cliente ";
            sql = sql + " where ClienteNulo = 1";
            DataTable trdo = cDb.ExecuteDataTable(sql);
            if (trdo.Rows.Count > 0)
            {
                if (trdo.Rows[0]["CodCliente"].ToString() != "")
                {
                    CodigoCliente = Convert.ToInt32(trdo.Rows[0]["CodCliente"].ToString());
                    ban = 1;
                }
            }
            if (ban == 0)
            {
                 sql = "Insert into Cliente(ClienteNulo)";
                sql = sql + "values(1)";
                CodigoCliente = cDb.EjecutarEscalar(sql);
            }
            return CodigoCliente;
        }
    }
}

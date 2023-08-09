using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data; 
namespace PELUQUERIA.Clases
{
    public  class cPeluquero
    {
        public DataTable GetPeluqueros()
        {
            string sql = "Select CodPeluquero, (Apellido + ' ' + Nombre) as Apellido";
            sql = sql + " from Peluquero ";
            sql = sql + " order by Apellido ";
            return cDb.ExecuteDataTable(sql);
        }

        public DataTable GetPeluquerosActivos()
        {
            string sql = "Select CodPeluquero, (Apellido + ' ' + Nombre) as Apellido";
            sql = sql + " from Peluquero ";
            sql = sql + " where Inactivo is null or Inactivo =0";
            sql = sql + " order by Apellido ";
            return cDb.ExecuteDataTable(sql);
        }
    }
}

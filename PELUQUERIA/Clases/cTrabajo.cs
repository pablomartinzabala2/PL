using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace PELUQUERIA.Clases
{
    public  class cTrabajo
    {
        public DataTable GetTrabajosActivos()
        {
            string sql = " select * from Trabajo ";
            sql = sql + " where Inactivo is null or Inactivo =0";
            sql = sql + " order by Nombre";
            return cDb.ExecuteDataTable(sql);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace PELUQUERIA.Clases
{
    public class cProducto
    {
        public DataTable GetProductosActivos()
        {
            string sql = " select * from Producto ";
            sql = sql + " where Inactivo is null or Inactivo =0";
            return cDb.ExecuteDataTable(sql);
        }
    }
}

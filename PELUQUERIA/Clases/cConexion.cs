using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PELUQUERIA.Clases
{
    public static  class cConexion
    {
        public static string Cadenacon()
        {
            //*****CASA**********
            //  string cadena = "Data Source=DESKTOP-QKECIIE;Initial Catalog=PL;Integrated Security=True";
            //  string cadena = "Data Source=DESKTOP-HE27MBV\\PABLO;Initial Catalog=PELUQUERIA;Integrated Security=True";
            string cadena = PELUQUERIA.Properties.Settings.Default.PELUQUERIAConnectionString;

            return cadena;
        }
    }
}

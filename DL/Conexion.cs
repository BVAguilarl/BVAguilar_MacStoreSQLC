using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class Conexion
    {
        public static string GetConecion()
        {
            string cadena = System.Configuration.ConfigurationManager.ConnectionStrings["BVAguilar_MacStore"].ConnectionString;
            return cadena;
        }
    }
}

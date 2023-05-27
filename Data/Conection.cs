using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace SIPYME.Data
{
    public class Conection
    {
        public static string cn = /*ConfigurationManager.ConnectionStrings["cadena"].ToString();*/"server=localhost;user=root;database=db_sipyme;port=3306;password=root";

    }
}
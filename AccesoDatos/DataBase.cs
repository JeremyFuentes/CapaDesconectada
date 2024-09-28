using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    internal class DataBase
    {
        public static string ConnectcionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["NWConnection"].ConnectionString;
            }
        }

        public static SqlConnection GetSqlConnection()
        {
            SqlConnection conexion = new SqlConnection(ConnectcionString);
            conexion.Open();
            return conexion;
        }
    }
}

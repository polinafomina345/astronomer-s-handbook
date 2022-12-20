using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace astronomer_s_handbook
{
    internal class DataBase
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-BMMKT88;Initial Catalog=astronomer'_handbook; Integrated Security = true");

        public void openConnection()
        {
            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }

        }
        public void closeConnection()
        {
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
        }

        public SqlConnection getConnection()
        {
            return conn;
        }
    }
}

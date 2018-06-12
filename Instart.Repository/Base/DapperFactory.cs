using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Repository
{

    public class DapperFactory
    {
        public static IDbConnection GetConnection()
        {
            return new SqlConnection(AppSettings.ConnectionString);
        }
    }
}

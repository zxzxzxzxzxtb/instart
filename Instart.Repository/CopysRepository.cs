using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Instart.Models;

namespace Instart.Repository
{
    public class CopysRepository : ICopysRepository
    {
        public int GetCountAsync()
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select count(1) from [Copys];";
                return conn.ExecuteScalar<int>(sql);
            }
        }

        public Copys GetInfoAsync()
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select * from [Copys];";
                return conn.QueryFirstOrDefault<Copys>(sql);
            }
        }

        public bool InsertAsync(Copys model)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                var fields = model.ToFields();
                if (fields == null || fields.Count == 0)
                {
                    return false;
                }

                string sql = string.Format("insert into [Copys] ({0}) values ({1});", string.Join(",", fields), string.Join(",", fields.Select(n => "@" + n)));
                return conn.Execute(sql, model) > 0;
            }
        }

        public bool UpdateAsync(Copys model)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                var fields = model.ToFields();

                if (fields == null || fields.Count == 0)
                {
                    return false;
                }

                var fieldList = new List<string>();
                foreach (var field in fields)
                {
                    fieldList.Add(string.Format("{0}=@{0}",field));
                }

                string sql = string.Format("update [Copys] set {0};", string.Join(",", fieldList));
                return conn.Execute(sql, model) > 0;
            }
        }
    }
}

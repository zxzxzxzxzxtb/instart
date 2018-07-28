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
    public class RecruitRepository : IRecruitRepository
    {
        public int GetCountAsync()
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select count(1) from [Recruit];";
                return conn.ExecuteScalar<int>(sql);
            }
        }

        public Recruit GetInfoAsync()
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select * from [Recruit];";
                return conn.QueryFirstOrDefault<Recruit>(sql);
            }
        }

        public bool InsertAsync(Recruit model)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                var fields = model.ToFields();
                if (fields == null || fields.Count == 0)
                {
                    return false;
                }

                string sql = $"insert into [Recruit] ({string.Join(",", fields)}) values ({string.Join(",", fields.Select(n => "@" + n))});";
                return conn.Execute(sql, model) > 0;
            }
        }

        public bool UpdateAsync(Recruit model)
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
                    fieldList.Add($"{field}=@{field}");
                }

                string sql = $"update [Recruit] set {string.Join(",", fieldList)};";
                return conn.Execute(sql, model) > 0;
            }
        }
    }
}

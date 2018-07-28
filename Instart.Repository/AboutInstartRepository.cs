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
    public class AboutInstartRepository : IAboutInstartRepository
    {
        public int GetCountAsync()
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select count(1) from [AboutInstart];";
                return conn.ExecuteScalar<int>(sql);
            }
        }

        public AboutInstart GetInfoAsync()
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select * from [AboutInstart];";
                return conn.QueryFirstOrDefault<AboutInstart>(sql);
            }
        }

        public bool InsertAsync(AboutInstart model)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                var fields = model.ToFields();
                if (fields == null || fields.Count == 0)
                {
                    return false;
                }

                model.CreateTime = DateTime.Now;
                model.ModifyTime = DateTime.Now;

                string sql = $"insert into [AboutInstart] ({string.Join(",", fields)}) values ({string.Join(",", fields.Select(n => "@" + n))});";
                return conn.Execute(sql, model) > 0;
            }
        }

        public bool UpdateAsync(AboutInstart model)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                var fields = model.ToFields(removeFields: new List<string>
                {
                    nameof(model.CreateTime)
                });

                if (fields == null || fields.Count == 0)
                {
                    return false;
                }

                var fieldList = new List<string>();
                foreach (var field in fields)
                {
                    fieldList.Add($"{field}=@{field}");
                }

                model.ModifyTime = DateTime.Now;

                string sql = $"update [AboutInstart] set {string.Join(",", fieldList)};";
                return conn.Execute(sql, model) > 0;
            }
        }
    }
}

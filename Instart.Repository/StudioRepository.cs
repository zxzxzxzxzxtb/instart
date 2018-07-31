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
    public class StudioRepository : IStudioRepository
    {
        public int GetCountAsync()
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select count(1) from [Studio];";
                return conn.ExecuteScalar<int>(sql);
            }
        }

        public Studio GetInfoAsync()
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select * from [Studio];";
                return conn.QueryFirstOrDefault<Studio>(sql);
            }
        }

        public bool InsertAsync(Studio model)
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

                string sql = string.Format("insert into [Studio] ({0}) values ({1});", string.Join(",", fields), string.Join(",", fields.Select(n => "@" + n)));
                return conn.Execute(sql, model) > 0;
            }
        }

        public bool UpdateAsync(Studio model)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                var fields = model.ToFields(removeFields: new List<string>
                {
                    "CreateTime"
                });

                if (fields == null || fields.Count == 0)
                {
                    return false;
                }

                var fieldList = new List<string>();
                foreach (var field in fields)
                {
                    fieldList.Add(string.Format("{0}=@{0}", field));
                }

                model.ModifyTime = DateTime.Now;

                string sql = string.Format("update [Studio] set {0};", string.Join(",", fieldList));
                return conn.Execute(sql, model) > 0;
            }
        }

        public IEnumerable<StudioImg> GetImgsAsync()
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = string.Format(@"select * from [StudioImg];");
                return conn.Query<StudioImg>(sql);
            }
        }

        public bool InsertImgAsync(string imgUrl)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = string.Format("insert into [StudioImg] (Imgurl) values ('{0}');", imgUrl);
                return conn.Execute(sql) > 0;
            }
        }

        public bool DeleteImgAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "delete from [StudioImg] where Id=@Id;";
                return conn.Execute(sql, new { Id = id }) > 0;
            }
        }
    }
}

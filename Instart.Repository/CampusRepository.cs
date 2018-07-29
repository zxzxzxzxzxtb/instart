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
    public class CampusRepository : ICampusRepository
    {
        public Campus GetByIdAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select * from [Campus] where Id = @Id and Status=1;";
                return conn.QueryFirstOrDefault<Campus>(sql, new { Id = id });
            }
        }

        public PageModel<Campus> GetListAsync(int pageIndex, int pageSize, string name = null)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                #region generate condition
                string where = "where Status=1";
                if (!string.IsNullOrEmpty(name))
                {
                    where += string.Format(" and Name like '%{0}%'",name);
                }
                #endregion

                string countSql = string.Format("select count(1) from [Campus] {0};",where);
                int total = conn.ExecuteScalar<int>(countSql);
                if (total == 0)
                {
                    return new PageModel<Campus>();
                }

                string sql = string.Format(@"select * from (
                             select *, ROW_NUMBER() over (Order by Id desc) as RowNumber from [Campus] {0}
                             ) as b
                             where RowNumber between {1} and {2};",where,((pageIndex - 1) * pageSize) + 1,pageIndex * pageSize);
                var list = conn.Query<Campus>(sql);

                return new PageModel<Campus>
                {
                    Total = total,
                    Data = list != null ? list.ToList() : null
                };
            }
        }

        public IEnumerable<Campus> GetAllAsync()
        {
            using (var conn = DapperFactory.GetConnection())
            {
                #region generate condition
                string where = "where Status=1";
                #endregion

                string sql = string.Format(@"select * from [Campus] {0};",where);
                return conn.Query<Campus>(sql);
            }
        }

        public bool InsertAsync(Campus model)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                var fields = model.ToFields(removeFields: new List<string> { "Id" });
                if (fields == null || fields.Count == 0)
                {
                    return false;
                }

                model.CreateTime = DateTime.Now;
                model.ModifyTime = DateTime.Now;
                model.Status = 1;

                string sql = string.Format("insert into [Campus] ({0}) values ({1});", string.Join(",", fields), string.Join(",", fields.Select(n => "@" + n)));
                return conn.Execute(sql, model) > 0;
            }
        }

        public bool UpdateAsync(Campus model)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                List<string> removeFields = new List<string>
                {
                    "Id",
                    "CreateTime",
                    "Status"
                };
                var fields = model.ToFields(removeFields: removeFields);

                if (fields == null || fields.Count == 0)
                {
                    return false;
                }

                var fieldList = new List<string>();
                foreach (var field in fields)
                {
                    fieldList.Add(string.Format("{0}=@{0}",field));
                }

                model.ModifyTime = DateTime.Now;

                string sql = string.Format("update [Campus] set {0} where Id=@Id;",string.Join(",", fieldList));
                return conn.Execute(sql, model) > 0;
            }
        }

        public bool DeleteAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "update [Campus] set Status=0,ModifyTime=GETDATE() where Id=@Id;";
                return conn.Execute(sql, new { Id = id }) > 0;
            }
        }

        public IEnumerable<CampusImg> GetImgsByCampusIdAsync(int campusId)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                #region generate condition
                string where = string.Format("where Status=1 and CampusId = {0}",campusId);
                #endregion

                string sql = string.Format(@"select * from [CampusImg] {0};", where);
                return conn.Query<CampusImg>(sql);
            }
        }

        public bool InsertImgAsync(CampusImg model)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                var fields = model.ToFields(removeFields: new List<string> { "Id"});
                if (fields == null || fields.Count == 0)
                {
                    return false;
                }

                model.CreateTime = DateTime.Now;
                model.ModifyTime = DateTime.Now;
                model.Status = 1;

                string sql = string.Format("insert into [CampusImg] ({0}) values ({1});",string.Join(",", fields,string.Join(",", fields.Select(n => "@" + n))));
                return conn.Execute(sql, model) > 0;
            }
        }

        public bool DeleteImgAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "update [CampusImg] set Status=0,ModifyTime=GETDATE() where Id=@Id;";
                return conn.Execute(sql, new { Id = id }) > 0;
            }
        }
    }
}

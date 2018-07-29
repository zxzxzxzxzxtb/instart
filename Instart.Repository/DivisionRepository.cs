using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Instart.Models;

namespace Instart.Repository
{
    public class DivisionRepository : IDivisionRepository
    {
        public Division GetByIdAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select * from [Division] where Id = @Id and Status=1;";
                return conn.QueryFirstOrDefault<Division>(sql, new { Id = id });
            }
        }

        public PageModel<Division> GetListAsync(int pageIndex, int pageSize, string name = null)
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

                string countSql = string.Format("select count(1) from [Division] {0};",where);
                int total = conn.ExecuteScalar<int>(countSql);
                if (total == 0)
                {
                    return new PageModel<Division>();
                }

                string sql = string.Format(@"select * from (
                             select *, ROW_NUMBER() over (Order by Id desc) as RowNumber from [Division] {0}
                             ) as b
                             where RowNumber between {1} and {2};",where,((pageIndex - 1) * pageSize) + 1,pageIndex * pageSize);
                var list = conn.Query<Division>(sql);

                return new PageModel<Division>
                {
                    Total = total,
                    Data = list != null ? list.ToList() : null
                };
            }
        }

        public IEnumerable<Division> GetAllAsync()
        {
            using (var conn = DapperFactory.GetConnection())
            {
                #region generate condition
                string where = "where Status=1";
                #endregion

                string sql = string.Format(@"select * from [Division] {0};",where);
                return conn.Query<Division>(sql);
            }
        }

        public bool InsertAsync(Division model)
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

                string sql = string.Format("insert into [Division] ({0}) values ({1});",string.Join(",", fields),string.Join(",", fields.Select(n => "@" + n)));
                return conn.Execute(sql, model) > 0;
            }
        }

        public bool UpdateAsync(Division model)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                var fields = model.ToFields(removeFields: new List<string>
                {
                    "Id",
                    "CreateTime",
                    "Status"
                });

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

                string sql = string.Format("update [Division] set {0} where Id=@Id;",string.Join(",", fieldList));
                return conn.Execute(sql, model) > 0;
            }
        }

        public bool DeleteAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "update [Division] set Status=0,ModifyTime=GETDATE() where Id=@Id;";
                return conn.Execute(sql, new { Id = id }) > 0;
            }
        }
    }
}

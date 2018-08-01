using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instart.Models;
using Dapper;

namespace Instart.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        public Company GetByIdAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select * from [Company] where Id = @Id and Status=1;";
                return conn.QueryFirstOrDefault<Company>(sql, new { Id = id });
            }
        }

        public PageModel<Company> GetListAsync(int pageIndex, int pageSize, string name = null)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                #region generate condition
                string where = "where a.Status=1";
                if (!string.IsNullOrEmpty(name))
                {
                    where += string.Format(" and a.Name like '%{0}%'",name);
                }
                #endregion

                string countSql = string.Format("select count(1) from [Company] as a {0};", where);
                int total = conn.ExecuteScalar<int>(countSql);
                if (total == 0)
                {
                    return new PageModel<Company>();
                }

                string sql = string.Format(@"select * from (
                     select a.*, ROW_NUMBER() over (Order by a.Id desc) as RowNumber from [Company] as a {0}
                     ) as c
                     where RowNumber between {1} and {2};", where,((pageIndex - 1) * pageSize) + 1, pageIndex * pageSize);
                var list = conn.Query<Company>(sql);

                return new PageModel<Company>
                {
                    Total = total,
                    Data = list != null ? list.ToList() : null
                };
            }
        }

        public IEnumerable<Company> GetAllAsync()
        {
            using (var conn = DapperFactory.GetConnection())
            {
                #region generate condition
                string where = "where Status=1";
                #endregion

                string sql = string.Format(@"select * from [Company] {0};", where);
                return conn.Query<Company>(sql);
            }
        }

        public bool InsertAsync(Company model)
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

                string sql = string.Format("insert into [Company] ({0}) values ({1});", string.Join(",", fields), string.Join(",", fields.Select(n => "@" + n)));
                return conn.Execute(sql, model) > 0;
            }
        }

        public bool UpdateAsync(Company model)
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
                    fieldList.Add(string.Format("{0}=@{0}", field));
                }

                model.ModifyTime = DateTime.Now;

                string sql = string.Format("update [Company] set {0} where Id=@Id;", string.Join(",", fieldList));
                return conn.Execute(sql, model) > 0;
            }
        }

        public bool DeleteAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "update [Company] set Status=0,ModifyTime=GETDATE() where Id=@Id;";
                return conn.Execute(sql, new { Id = id }) > 0;
            }
        }
    }
}

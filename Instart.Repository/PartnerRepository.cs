using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Instart.Models;

namespace Instart.Repository
{
    public class PartnerRepository : IPartnerRepository
    {
        public Partner GetByIdAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select * from [Partner] where Id = @Id and Status=1;";
                return conn.QueryFirstOrDefault<Partner>(sql, new { Id = id });
            }
        }

        public PageModel<Partner> GetListAsync(int pageIndex, int pageSize, string name = null)
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

                string countSql = string.Format("select count(1) from [Partner] {0};",where);
                int total = conn.ExecuteScalar<int>(countSql);
                if (total == 0)
                {
                    return new PageModel<Partner>();
                }

                string sql = string.Format(@"select * from (
                             select *, ROW_NUMBER() over (Order by Id desc) as RowNumber from [Partner] {0}
                             ) as b
                             where RowNumber between {1} and {2};",where,((pageIndex - 1) * pageSize) + 1, pageIndex * pageSize);
                var list = conn.Query<Partner>(sql);

                return new PageModel<Partner>
                {
                    Total = total,
                    Data = list != null ? list.ToList() : null
                };
            }
        }

        public bool InsertAsync(Partner model)
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

                string sql = string.Format("insert into [Partner] ({0}) values ({1});", string.Join(",", fields), string.Join(",", fields.Select(n => "@" + n)));
                return conn.Execute(sql, model) > 0;
            }
        }

        public bool UpdateAsync(Partner model)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                List<string> removeFields = new List<string>
                {
                    "Id",
                    "CreateTime",
                    "Status"
                };
                if (String.IsNullOrEmpty(model.ImageUrl))
                {
                    removeFields.Add("ImageUrl");
                }
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

                string sql = string.Format("update [Partner] set {0} where Id=@Id;", string.Join(",", fieldList));
                return conn.Execute(sql, model) > 0;
            }
        }

        public bool DeleteAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "update [Partner] set Status=0,ModifyTime=GETDATE() where Id=@Id;";
                return conn.Execute(sql, new { Id = id }) > 0;
            }
        }

        public List<Partner> GetRecommendListAsync(int topCount)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = string.Format("select top {0} Id,Name,ImageUrl,Link from [Partner] where Status=1 order by Id desc;",topCount);
                var list = conn.Query<Partner>(sql, null);
                return list != null ? list.ToList() : null;
            }
        }
    }
}

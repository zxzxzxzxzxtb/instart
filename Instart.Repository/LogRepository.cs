using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Instart.Models;

namespace Instart.Repository
{
    public class LogRepository : ILogRepository
    {
        public PageModel<Log> GetListAsync(int pageIndex, int pageSize, string title, int userId, int type)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                #region generate condition
                string where = "where 1=1";
                if (!string.IsNullOrEmpty(title))
                {
                    where += string.Format(" and Title like '%{0}%'",title);
                }
                if (userId > 0)
                {
                    where += string.Format(" and UserId = {0}", userId);
                }
                if(type > -1)
                {
                    where += string.Format(" and Type = {0}", type);
                }
                #endregion

                string countSql = string.Format("select count(1) from Log {0}", where);
                int total = conn.ExecuteScalar<int>(countSql);
                if (total == 0)
                {
                    return new PageModel<Log>();
                }

                string sql = string.Format(@"select * from ( select *, ROW_NUMBER() over (Order by Id desc) as RowNumber from Log {0} ) as b where RowNumber between {1} and {2};",where,((pageIndex - 1) * pageSize) + 1,pageIndex * pageSize);
                var list = conn.Query<Log>(sql);

                return new PageModel<Log>
                {
                    Total = total,
                    Data = list != null ? list.ToList() : null
                };
            }
        }

        public List<Log> GetTopListAsync(int topCount)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = string.Format("select top {0} * from Log Order by CreateTime desc;", topCount);
                var list = conn.Query<Log>(sql, null);
                return list != null ? list.ToList() : null;
            }
        }

        public bool Insert(Log model)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                var fields = model.ToFields(removeFields: new List<string> { "Id" });
                if (fields == null || fields.Count == 0)
                {
                    return false;
                }

                model.CreateTime = DateTime.Now;

                string sql = string.Format("insert into Log ({0}) values ({1});", string.Join(",", fields), string.Join(",", fields.Select(n => "@" + n)));
                return conn.Execute(sql, model) > 0;
            }
        }
    }
}

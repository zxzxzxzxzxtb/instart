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
        public async Task<PageModel<Log>> GetListAsync(int pageIndex, int pageSize, string title, int userId, int type)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                #region generate condition
                string where = "where 1=1";
                if (!string.IsNullOrEmpty(title))
                {
                    where += $" and Title like '%{title}%'";
                }
                if (userId > 0)
                {
                    where += $" and UserId = {userId}";
                }
                if(type > -1)
                {
                    where += $" and Type = {type}";
                }
                #endregion

                string countSql = $"select count(1) from Log {where};";
                int total = await conn.ExecuteScalarAsync<int>(countSql);
                if (total == 0)
                {
                    return new PageModel<Log>();
                }

                string sql = $@"select * from ( select *, ROW_NUMBER() over (Order by Id desc) as RowNumber from Log {where} ) as b where RowNumber between {((pageIndex - 1) * pageSize) + 1} and {pageIndex * pageSize};";
                var list = await conn.QueryAsync<Log>(sql);

                return new PageModel<Log>
                {
                    Total = total,
                    Data = list?.ToList()
                };
            }
        }

        public async Task<List<Log>> GetTopListAsync(int topCount)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = $"select top {topCount} * from Log Order by CreateTime desc;";
                return (await conn.QueryAsync<Log>(sql, null))?.ToList();
            }
        }

        public bool Insert(Log model)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                var fields = model.ToFields(removeFields: new List<string> { nameof(model.Id) });
                if (fields == null || fields.Count == 0)
                {
                    return false;
                }

                model.CreateTime = DateTime.Now;

                string sql = $"insert into Log ({string.Join(",", fields)}) values ({string.Join(",", fields.Select(n => "@" + n))});";
                return conn.Execute(sql, model) > 0;
            }
        }
    }
}

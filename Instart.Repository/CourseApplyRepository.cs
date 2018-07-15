using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Instart.Models;
using Instart.Models.Enums;

namespace Instart.Repository
{
    public class CourseApplyRepository : ICourseApplyRepository
    {
        public async Task<List<string>> GetApplyCourseNameListAsync()
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select CourseName from CourseApply group by CourseName;";
                return (await conn.QueryAsync<string>(sql, null))?.ToList();
            }
        }

        public async Task<PageModel<CourseApply>> GetListAsync(int pageIndex, int pageSize, string courseName, EnumAccept accept)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                #region generate condition
                string where = "where 1=1 ";
                if (!string.IsNullOrEmpty(courseName))
                {
                    where += $" and CourseName like '%{courseName}%'";
                }
                if(accept != EnumAccept.All)
                {
                    where += $" and IsAccept = {(int)accept}";
                }
                #endregion

                string countSql = $"select count(1) from [CourseApply] {where};";
                int total = await conn.ExecuteScalarAsync<int>(countSql);
                if (total == 0)
                {
                    return new PageModel<CourseApply>();
                }

                string sql = $@"select * from ( select *, ROW_NUMBER() over (Order by Id desc) as RowNumber from [CourseApply] {where} ) as b where RowNumber between {((pageIndex - 1) * pageSize) + 1} and {pageIndex * pageSize};";
                var list = await conn.QueryAsync<CourseApply>(sql);

                return new PageModel<CourseApply>
                {
                    Total = total,
                    Data = list?.ToList()
                };
            }
        }

        public async Task<bool> InsertAsync(CourseApply model)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                var fields = model.ToFields(removeFields: new List<string> { nameof(model.Id) });
                if (fields == null || fields.Count == 0)
                {
                    return false;
                }

                model.CreateTime = DateTime.Now;

                string sql = $"insert into [CourseApply] ({string.Join(",", fields)}) values ({string.Join(",", fields.Select(n => "@" + n))});";
                return await conn.ExecuteAsync(sql, model) > 0;
            }
        }

        public async Task<bool> SetAcceptAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "update [CourseApply] set IsAccept=1,AcceptTime=GETDATE() where Id=@Id;";
                return await conn.ExecuteAsync(sql, new { Id = id }) > 0;
            }
        }
    }
}

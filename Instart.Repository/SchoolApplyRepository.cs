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
    public class SchoolApplyRepository : ISchoolApplyRepository
    {
        public List<string> GetApplySchoolNameListAsync()
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select SchoolName from SchoolApply group by SchoolName;";
                return (conn.Query<string>(sql, null))?.ToList();
            }
        }

        public PageModel<SchoolApply> GetListAsync(int pageIndex, int pageSize, string schoolName, EnumAccept accept)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                #region generate condition
                string where = "where 1=1 ";
                if (!string.IsNullOrEmpty(schoolName))
                {
                    where += $" and SchoolName like '%{schoolName}%'";
                }
                if(accept != EnumAccept.All)
                {
                    where += $" and IsAccept = {(int)accept}";
                }
                #endregion

                string countSql = $"select count(1) from [SchoolApply] {where};";
                int total = conn.ExecuteScalar<int>(countSql);
                if (total == 0)
                {
                    return new PageModel<SchoolApply>();
                }

                string sql = $@"select * from ( select *, ROW_NUMBER() over (Order by Id desc) as RowNumber from [SchoolApply] {where} ) as b where RowNumber between {((pageIndex - 1) * pageSize) + 1} and {pageIndex * pageSize};";
                var list = conn.Query<SchoolApply>(sql);

                return new PageModel<SchoolApply>
                {
                    Total = total,
                    Data = list?.ToList()
                };
            }
        }

        public bool InsertAsync(SchoolApply model)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                var fields = model.ToFields(removeFields: new List<string> { nameof(model.Id) });
                if (fields == null || fields.Count == 0)
                {
                    return false;
                }

                model.CreateTime = DateTime.Now;

                string sql = $"insert into [SchoolApply] ({string.Join(",", fields)}) values ({string.Join(",", fields.Select(n => "@" + n))});";
                return conn.Execute(sql, model) > 0;
            }
        }

        public bool SetAcceptAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "update [SchoolApply] set IsAccept=1,AcceptTime=GETDATE() where Id=@Id;";
                return conn.Execute(sql, new { Id = id }) > 0;
            }
        }

        public List<SchoolApply> GetTopListAsync(int topCount)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = $"select top {topCount} * from SchoolApply order by Id Desc;";
                return (conn.Query<SchoolApply>(sql, null))?.ToList();
            }
        }
    }
}

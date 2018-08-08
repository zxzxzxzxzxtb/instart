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
    public class ProgramApplyRepository : IProgramApplyRepository
    {
        public PageModel<ProgramApply> GetListAsync(int pageIndex, int pageSize, string programName, EnumAccept accept)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                #region generate condition
                string where = "where 1=1 ";
                if (!string.IsNullOrEmpty(programName))
                {
                    where += string.Format(" and c.Name like '%{0}%'", programName);
                }
                if(accept != EnumAccept.All)
                {
                    where += string.Format(" and a.IsAccept = {0}",(int)accept);
                }
                #endregion

                string countSql = string.Format("select count(1) from [ProgramApply] as a left join [Program] as c on c.Id = a.ProgramId {0};", where);
                int total =  conn.ExecuteScalar<int>(countSql);
                if (total == 0)
                {
                    return new PageModel<ProgramApply>();
                }

                string sql = string.Format(@"select * from ( select a.*, m.Name as MajorName, m.NameEn as MajorNameEn, 
                    c.Name as ProgramName, c.NameEn as ProgramNameEn, c.Type as ProgramType, ROW_NUMBER() over (Order by a.Id desc) as RowNumber from [ProgramApply] as a
                left join [Program] as c on c.Id = a.ProgramId 
                left join [Major] m on m.Id = a.MajorId {0} ) as b where RowNumber between {1} and {2};", where, ((pageIndex - 1) * pageSize) + 1, pageIndex * pageSize);
                var list = conn.Query<ProgramApply>(sql);

                return new PageModel<ProgramApply>
                {
                    Total = total,
                    Data = list != null ? list.ToList() : null
                };
            }
        }

        public bool InsertAsync(ProgramApply model)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                var fields = model.ToFields(removeFields: new List<string> { "Id", "MajorName", "MajorNameEn", "ProgramName", "ProgramNameEn", "ProgramType" });
                if (fields == null || fields.Count == 0)
                {
                    return false;
                }

                model.CreateTime = DateTime.Now;

                string sql = string.Format("insert into [ProgramApply] ({0}) values ({1});", string.Join(",", fields), string.Join(",", fields.Select(n => "@" + n)));
                return  conn.Execute(sql, model) > 0;
            }
        }

        public  bool SetAcceptAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "update [ProgramApply] set IsAccept=1,AcceptTime=GETDATE() where Id=@Id;";
                return  conn.Execute(sql, new { Id = id }) > 0;
            }
        }

        public List<ProgramApply> GetTopListAsync(int topCount)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = string.Format("select top {0} * from ProgramApply order by Id Desc;", topCount);
                var list = conn.Query<ProgramApply>(sql, null);
                return list != null ? list.ToList() : null;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instart.Models;
using Dapper;

namespace Instart.Repository
{
    public class WorksRepository : IWorksRepository
    {
        public async Task<Works> GetByIdAsync(int id) {
            using (var conn = DapperFactory.GetConnection()) {
                string sql = "select * from [Works] where Id = @Id and Status=1;";
                return await conn.QueryFirstOrDefaultAsync<Works>(sql, new { Id = id });
            }
        }

        public async Task<PageModel<Works>> GetListAsync(int pageIndex, int pageSize, string name = null) {
            using (var conn = DapperFactory.GetConnection()) {
                #region generate condition
                string where = "where a.Status=1";
                #endregion

                string countSql = $"select count(1) from [Works] as a {where};";
                int total = await conn.ExecuteScalarAsync<int>(countSql);
                if (total == 0) {
                    return new PageModel<Works>();
                }

                string sql = $@"select * from (select a.*, b.Name as MajorName, ROW_NUMBER() over (Order by a.Id desc) as RowNumber from [Works] as a 
                    left join [Major] as b on b.Id = a.MajorId {where}) as c 
                    where RowNumber between {((pageIndex - 1) * pageSize) + 1} and {pageIndex * pageSize};";
                var list = await conn.QueryAsync<Works>(sql);

                return new PageModel<Works> {
                    Total = total,
                    Data = list?.ToList()
                };
            }
        }

        public async Task<List<Works>> GetListByMajorIdAsync(int majorId, int topCount)
        {
            using(var conn = DapperFactory.GetConnection())
            {
                string sql = $"select top {topCount} Id,Name,ImgUrl,Introduce,CreateTime from Works where MajorId=@MajorId and Status=1 order by Id desc";
                return (await conn.QueryAsync<Works>(sql,new { MajorId = majorId}))?.ToList();
            }
        }

        public async Task<bool> InsertAsync(Works model) {
            using (var conn = DapperFactory.GetConnection()) {
                var fields = model.ToFields(removeFields: new List<string> { nameof(model.Id), nameof(model.MajorName) });
                if (fields == null || fields.Count == 0) {
                    return false;
                }

                model.CreateTime = DateTime.Now;
                model.ModifyTime = DateTime.Now;
                model.Status = 1;

                string sql = $"insert into [Works] ({string.Join(",", fields)}) values ({string.Join(",", fields.Select(n => "@" + n))});";
                return await conn.ExecuteAsync(sql, model) > 0;
            }
        }

        public async Task<bool> UpdateAsync(Works model) {
            using (var conn = DapperFactory.GetConnection()) {
                List<string> removeFields = new List<string>
                {
                    nameof(model.Id),
                    nameof(model.MajorName),
                    nameof(model.CreateTime),
                    nameof(model.Status)
                };
                if (String.IsNullOrEmpty(model.ImgUrl))
                {
                    removeFields.Add(nameof(model.ImgUrl));
                }
                var fields = model.ToFields(removeFields: removeFields);

                if (fields == null || fields.Count == 0) {
                    return false;
                }

                var fieldList = new List<string>();
                foreach (var field in fields) {
                    fieldList.Add($"{field}=@{field}");
                }

                model.ModifyTime = DateTime.Now;

                string sql = $"update [Works] set {string.Join(",", fieldList)} where Id=@Id;";
                return await conn.ExecuteAsync(sql, model) > 0;
            }
        }

        public async Task<bool> DeleteAsync(int id) {
            using (var conn = DapperFactory.GetConnection()) {
                string sql = "update [Works] set Status=0,ModifyTime=GETDATE() where Id=@Id;";
                return await conn.ExecuteAsync(sql, new { Id = id }) > 0;
            }
        }
    }
}

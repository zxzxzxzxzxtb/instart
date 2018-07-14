using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instart.Models;
using Dapper;

namespace Instart.Repository
{
    public class MajorRepository : IMajorRepository
    {
        public async Task<Major> GetByIdAsync(int id) {
            using (var conn = DapperFactory.GetConnection()) {
                string sql = "select * from [Major] where Id = @Id and Status=1;";
                return await conn.QueryFirstOrDefaultAsync<Major>(sql, new { Id = id });
            }
        }

        public async Task<PageModel<Major>> GetListAsync(int pageIndex, int pageSize, string name = null) {
            using (var conn = DapperFactory.GetConnection()) {
                #region generate condition
                string where = "where a.Status=1";
                if (!string.IsNullOrEmpty(name)) {
                    where += $" and a.Name like '%{name}%'";
                }
                #endregion

                string countSql = $"select count(1) from [Major] as a {where};";
                int total = await conn.ExecuteScalarAsync<int>(countSql);
                if (total == 0) {
                    return new PageModel<Major>();
                }

                string sql = $@"select * from (
                     select a.*, b.Name as DivisionName, ROW_NUMBER() over (Order by a.Id desc) as RowNumber from [Major] as a
                     left join [Division] as b on b.Id = a.DivisionId {where}
                     ) as c
                     where RowNumber {((pageIndex - 1) * pageSize) + 1} and {pageIndex * pageSize};";
                var list = await conn.QueryAsync<Major>(sql);

                return new PageModel<Major> {
                    Total = total,
                    Data = list?.ToList()
                };
            }
        }

        public async Task<IEnumerable<Major>> GetAllAsync()
        {
            using (var conn = DapperFactory.GetConnection())
            {
                #region generate condition
                string where = "where Status=1";
                #endregion

                string sql = $@"select * from [Major] {where};";
                return await conn.QueryAsync<Major>(sql);
            }
        }

        public async Task<bool> InsertAsync(Major model) {
            using (var conn = DapperFactory.GetConnection()) {
                var fields = model.ToFields(removeFields: new List<string> { nameof(model.Id), nameof(model.DivisionName) });
                if (fields == null || fields.Count == 0) {
                    return false;
                }

                model.CreateTime = DateTime.Now;
                model.ModifyTime = DateTime.Now;
                model.Status = 1;

                string sql = $"insert into [Major] ({string.Join(",", fields)}) values ({string.Join(",", fields.Select(n => "@" + n))});";
                return await conn.ExecuteAsync(sql, model) > 0;
            }
        }

        public async Task<bool> UpdateAsync(Major model) {
            using (var conn = DapperFactory.GetConnection()) {
                List<string> removeFields = new List<string>
                {
                    nameof(model.Id),
                    nameof(model.DivisionName),
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

                string sql = $"update [Major] set {string.Join(",", fieldList)} where Id=@Id;";
                return await conn.ExecuteAsync(sql, model) > 0;
            }
        }

        public async Task<bool> DeleteAsync(int id) {
            using (var conn = DapperFactory.GetConnection()) {
                string sql = "update [Major] set Status=0,ModifyTime=GETDATE() where Id=@Id;";
                return await conn.ExecuteAsync(sql, new { Id = id }) > 0;
            }
        }
    }
}

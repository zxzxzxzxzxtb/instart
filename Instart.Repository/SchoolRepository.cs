using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instart.Models;
using Dapper;

namespace Instart.Repository
{
    public class SchoolRepository : ISchoolRepository
    {     
        public async Task<School> GetByIdAsync(int id) {
            using (var conn = DapperFactory.GetConnection()) {
                string sql = "select * from [School] where Id = @Id and Status=1;";
                return await conn.QueryFirstOrDefaultAsync<School>(sql, new { Id = id });
            }
        }

        public async Task<PageModel<School>> GetListAsync(int pageIndex, int pageSize, string name = null) {
            using (var conn = DapperFactory.GetConnection()) {
                #region generate condition
                string where = "where Status=1";
                if (!string.IsNullOrEmpty(name)) {
                    where += $" and Name like '%{name}%'";
                }
                #endregion

                string countSql = $"select count(1) from [School] {where};";
                int total = await conn.ExecuteScalarAsync<int>(countSql);
                if (total == 0) {
                    return new PageModel<School>();
                }

                string sql = $@"select * from ( select Id,Name,NameEn,Logo,Country,[Type],Address,Fee,Avatar,RecommendMajor,Difficult,TuoFu,YaSi,Yan,SAT,CreateTime,IsRecommend, ROW_NUMBER() over (Order by Id desc) as RowNumber from [School] {where} ) as b  
                                where RowNumber between {((pageIndex - 1) * pageSize) + 1} and {pageIndex * pageSize};";
                var list = await conn.QueryAsync<School>(sql);

                return new PageModel<School> {
                    Total = total,
                    Data = list?.ToList()
                };
            }
        }

        public async Task<bool> InsertAsync(School model) {
            using (var conn = DapperFactory.GetConnection()) {
                var fields = model.ToFields(removeFields: new List<string> { nameof(model.Id) });
                if (fields == null || fields.Count == 0) {
                    return false;
                }

                model.CreateTime = DateTime.Now;
                model.ModifyTime = DateTime.Now;
                model.Status = 1;

                string sql = $"insert into [School] ({string.Join(",", fields)}) values ({string.Join(",", fields.Select(n => "@" + n))});";
                return await conn.ExecuteAsync(sql, model) > 0;
            }
        }

        public async Task<bool> UpdateAsync(School model) {
            using (var conn = DapperFactory.GetConnection()) {
                var fields = model.ToFields(removeFields: new List<string>
                {
                    nameof(model.Id),
                    nameof(model.CreateTime),
                    nameof(model.Status)
                });

                if (fields == null || fields.Count == 0) {
                    return false;
                }

                var fieldList = new List<string>();
                foreach (var field in fields) {
                    fieldList.Add($"{field}=@{field}");
                }

                model.ModifyTime = DateTime.Now;

                string sql = $"update [School] set {string.Join(",", fieldList)} where Id=@Id;";
                return await conn.ExecuteAsync(sql, model) > 0;
            }
        }

        public async Task<bool> DeleteAsync(int id) {
            using (var conn = DapperFactory.GetConnection()) {
                string sql = "update [School] set Status=0,ModifyTime=GETDATE() where Id=@Id;";
                return await conn.ExecuteAsync(sql, new { Id = id }) > 0;
            }
        }

        public async Task<List<School>> GetRecommendListAsync(int topCount)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = $"select top {topCount} Id,Name,NameEn,Difficult,Avatar,Country,Fee,Scholarship from [School] where Status=1 and IsRecommend = 1 order by Id desc;";
                return (await conn.QueryAsync<School>(sql, null))?.ToList();
            }
        }

        public async Task<bool> SetRecommend(int id, bool isRecommend)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = $"update [School] set IsRecommend=@IsRecommend where Id=@Id;";
                return await conn.ExecuteAsync(sql, new { IsRecommend =isRecommend, Id = id}) > 0;
            }
        }
    }
}

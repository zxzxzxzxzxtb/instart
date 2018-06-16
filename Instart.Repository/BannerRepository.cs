using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instart.Models;
using Instart.Models.Enums;
using Dapper;

namespace Instart.Repository
{
    public class BannerRepository : IBannerRepository
    {
        public async Task<Banner> GetByIdAsync(int id) {
            using (var conn = DapperFactory.GetConnection()) {
                string sql = "select * from [Banner] where Id = @Id and Status=1;";
                return await conn.QueryFirstOrDefaultAsync<Banner>(sql, new { Id = id });
            }
        }

        public async Task<PageModel<Banner>> GetListAsync(int pageIndex, int pageSize, string title = null) {
            using (var conn = DapperFactory.GetConnection()) {
                #region generate condition
                string where = "where Status=1";
                if (!string.IsNullOrEmpty(title)) {
                    where += $" and Title like '%{title}%'";
                }
                #endregion

                string countSql = $"select count(1) from [Banner] {where};";
                int total = await conn.ExecuteScalarAsync<int>(countSql);
                if (total == 0) {
                    return new PageModel<Banner>();
                }

                string sql = $@"select * from (   
　　　　                            select *, ROW_NUMBER() over (Order by Id desc) as RowNumber from [Banner] {where} 
　　                            ) as b  
　　                            where RowNumber between {(pageIndex - 1) * pageIndex} and {pageIndex * pageIndex};";
                var list = await conn.QueryAsync<Banner>(sql);

                return new PageModel<Banner> {
                    Total = total,
                    Data = list?.ToList()
                };
            }
        }

        public async Task<List<Banner>> GetListByPosAsync(EnumBannerPos pos = EnumBannerPos.Index) {
            using (var conn = DapperFactory.GetConnection()) {
                string sql = "select * from [Banner] where Pos = @Pos and Status=1;";
                var list = await conn.QueryAsync<Banner>(sql, new { Pos = pos });
                return list?.ToList();
            }
        }

        public async Task<bool> InsertAsync(Banner model) {
            using (var conn = DapperFactory.GetConnection()) {
                var fields = model.ToFields(removeFields: new List<string> { nameof(model.Id) });
                if (fields == null || fields.Count == 0) {
                    return false;
                }

                string sql = $"insert into [Banner] ({string.Join(",", fields)}) values ({string.Join(",", fields.Select(n => "@" + n))});";
                return await conn.ExecuteAsync(sql, model) > 0;
            }
        }

        public async Task<bool> UpdateAsync(Banner model) {
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

                string sql = $"update [Banner] set {string.Join(",", fieldList)} where Id=@Id;";
                return await conn.ExecuteAsync(sql, model) > 0;
            }
        }

        public async Task<bool> DeleteAsync(int id) {
            using (var conn = DapperFactory.GetConnection()) {
                string sql = "update [Banner] set Status=0,ModifyTime=GETDATE() where Id=@Id;";
                return await conn.ExecuteAsync(sql, new { Id = id }) > 0;
            }
        }
    }
}

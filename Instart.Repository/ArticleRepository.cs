using Instart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Instart.Repository
{
    public class ArticleRepository : IArticleRepository
    {
        public async Task<Article> GetByIdAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select * from [Article] where Id = @Id and Status=1;";
                return await conn.QueryFirstOrDefaultAsync<Article>(sql, new { Id = id });
            }
        }

        public async Task<PageModel<Article>> GetListAsync(int pageIndex, int pageSize, int categoryId = 0, string title = null)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                #region generate condition
                string where = "where Status=1";
                if (categoryId > 0)
                {
                    where += $" and CategoryId={categoryId}";
                }
                if (!string.IsNullOrEmpty(title))
                {
                    where += $" and Title like '%{title}%'";
                } 
                #endregion

                string countSql = $"select count(1) from [Article] {where};";
                int total = await conn.ExecuteScalarAsync<int>(countSql);
                if (total == 0)
                {
                    return new PageModel<Article>();
                }

                var model = new Article();
                var fields = model.ToFields(removeFields: new List<string> { nameof(model.Content), nameof(model.Summary) });

                string sql = $@"select * from (   
　　　　                            select {string.Join(",", fields)}, ROW_NUMBER() over (Order by Id desc) as RowNumber from [Article] {where} 
　　                            ) as b  
　　                            where RowNumber between {(pageIndex - 1) * pageIndex} and {pageIndex * pageIndex};";
                var list = await conn.QueryAsync<Article>(sql);

                return new PageModel<Article>
                {
                    Total = total,
                    Data = list?.ToList()
                };
            }
        }

        public async Task<bool> InsertAsync(Article model)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                var fields = model.ToFields(removeFields: new List<string> { nameof(model.Id) });
                if (fields == null || fields.Count == 0)
                {
                    return false;
                }

                model.CreateTime = DateTime.Now;
                model.ModifyTime = DateTime.Now;
                model.Status = 1;

                string sql = $"insert into [Article] ({string.Join(",", fields)}) values ({string.Join(",", fields.Select(n => "@" + n))});";
                return await conn.ExecuteAsync(sql, model) > 0;
            }
        }

        public async Task<bool> UpdateAsync(Article model)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                var fields = model.ToFields(removeFields: new List<string>
                {
                    nameof(model.Id),
                    nameof(model.CreateTime),
                    nameof(model.Status)
                });

                if (fields == null || fields.Count == 0)
                {
                    return false;
                }

                var fieldList = new List<string>();
                foreach(var field in fields)
                {
                    fieldList.Add($"{field}=@{field}");
                }

                model.ModifyTime = DateTime.Now;

                string sql = $"update [Article] set {string.Join(",", fieldList)} where Id=@Id;";
                return await conn.ExecuteAsync(sql, model) > 0;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "update [Article] set Status=0,ModifyTime=GETDATE() where Id=@Id;";
                return await conn.ExecuteAsync(sql, new { Id = id }) > 0;
            }
        }
    }
}

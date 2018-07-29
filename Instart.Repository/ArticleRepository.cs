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
        public Article GetByIdAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select * from [Article] where Id = @Id and Status=1;";
                return conn.QueryFirstOrDefault<Article>(sql, new { Id = id });
            }
        }

        public PageModel<Article> GetListAsync(int pageIndex, int pageSize, int categoryId = 0, string title = null)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                #region generate condition
                string where = "where Status=1";
                if (categoryId > 0)
                {
                    where += string.Format(" and CategoryId={0}",categoryId);
                }
                if (!string.IsNullOrEmpty(title))
                {
                    where += string.Format(" and Title like '%{0}%'",title);
                } 
                #endregion

                string countSql = string.Format("select count(1) from [Article] {0};",where);
                int total = conn.ExecuteScalar<int>(countSql);
                if (total == 0)
                {
                    return new PageModel<Article>();
                }

                var model = new Article();
                var fields = model.ToFields(removeFields: new List<string> { "Content", "Summary" });

                string sql = string.Format(@"select * from (   
　　　　                            select {0}, ROW_NUMBER() over (Order by Id desc) as RowNumber from [Article] {1} 
　　                            ) as b  
　　                            where RowNumber between{2} and {3};",string.Join(",", fields),where,((pageIndex - 1) * pageSize) + 1,pageIndex * pageSize);
                var list = conn.Query<Article>(sql);

                return new PageModel<Article>
                {
                    Total = total,
                    Data = list != null ? list.ToList() : null
                };
            }
        }

        public bool InsertAsync(Article model)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                var fields = model.ToFields(removeFields: new List<string> { "Id" });
                if (fields == null || fields.Count == 0)
                {
                    return false;
                }

                model.CreateTime = DateTime.Now;
                model.ModifyTime = DateTime.Now;
                model.Status = 1;

                string sql = string.Format("insert into [Article] ({0}) values ({1});",string.Join(",", fields),string.Join(",", fields.Select(n => "@" + n)));
                return conn.Execute(sql, model) > 0;
            }
        }

        public bool UpdateAsync(Article model)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                var fields = model.ToFields(removeFields: new List<string>
                {
                    "Id",
                    "CreateTime",
                    "Status"
                });

                if (fields == null || fields.Count == 0)
                {
                    return false;
                }

                var fieldList = new List<string>();
                foreach(var field in fields)
                {
                    fieldList.Add(string.Format("{0}=@{0}",field));
                }

                model.ModifyTime = DateTime.Now;

                string sql = string.Format("update [Article] set {0} where Id=@Id;",string.Join(",", fieldList));
                return conn.Execute(sql, model) > 0;
            }
        }

        public bool DeleteAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "update [Article] set Status=0,ModifyTime=GETDATE() where Id=@Id;";
                return conn.Execute(sql, new { Id = id }) > 0;
            }
        }
    }
}

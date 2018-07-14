using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Instart.Models;

namespace Instart.Repository
{
    public class CampusRepository : ICampusRepository
    {
        public async Task<Campus> GetByIdAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select * from [Campus] where Id = @Id and Status=1;";
                return await conn.QueryFirstOrDefaultAsync<Campus>(sql, new { Id = id });
            }
        }

        public async Task<IEnumerable<string>> GetImgsByIdAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select ImgUrl from [CampusImg] where Id = @Id;";
                return await conn.QueryAsync<string>(sql, new { Id = id });
            }
        }

        public async Task<PageModel<Campus>> GetListAsync(int pageIndex, int pageSize, string name = null)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                #region generate condition
                string where = "where Status=1";
                if (!string.IsNullOrEmpty(name))
                {
                    where += $" and Name like '%{name}%'";
                }
                #endregion

                string countSql = $"select count(1) from [Campus] {where};";
                int total = await conn.ExecuteScalarAsync<int>(countSql);
                if (total == 0)
                {
                    return new PageModel<Campus>();
                }

                string sql = $@"select * from (
                             select *, ROW_NUMBER() over (Order by Id desc) as RowNumber from [Campus] {where}
                             ) as b
                             where RowNumber between {((pageIndex - 1) * pageSize) + 1} and {pageIndex * pageSize};";
                var list = await conn.QueryAsync<Campus>(sql);

                return new PageModel<Campus>
                {
                    Total = total,
                    Data = list?.ToList()
                };
            }
        }

        public async Task<IEnumerable<Campus>> GetAllAsync()
        {
            using (var conn = DapperFactory.GetConnection())
            {
                #region generate condition
                string where = "where Status=1";
                #endregion

                string sql = $@"select * from [Campus] {where};";
                return await conn.QueryAsync<Campus>(sql);
            }
        }

        public async Task<bool> InsertAsync(Campus model)
        {
            var result = 0;
            var id = 0;
            using (var conn = DapperFactory.GetConnection())
            {
                conn.Open();
                var tran = conn.BeginTransaction();
                var fields = model.ToFields(removeFields: new List<string> { nameof(model.Id), nameof(model.ImgUrls) });
                if (fields == null || fields.Count == 0)
                {
                    return false;
                }

                model.CreateTime = DateTime.Now;
                model.ModifyTime = DateTime.Now;
                model.Status = 1;

                string sql = $"insert into [Campus] ({string.Join(",", fields)}) values ({string.Join(",", fields.Select(n => "@" + n))});SELECT CAST(SCOPE_IDENTITY() AS BIGINT) AS [Id] ";

                var insertImg = @" INSERT INTO [CampusImg] ([Id],[Imgurl]) VALUES(@Id,@ImgUrl)";
                try
                {

                    id = await conn.ExecuteScalarAsync<int>(sql, model, tran);
                    if (model.ImgUrls != null)
                    {
                        foreach (var item in model.ImgUrls)
                        {
                            result = await conn.ExecuteAsync(insertImg, new { Id = id, ImgUrl = item }, tran);
                        }
                    }
                    tran.Commit();
                }
                catch (SqlException ex)
                {
                    tran.Rollback();
                    return false;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return false;
                }
            }//end using
            return result > 0;
        }

        public async Task<bool> UpdateAsync(Campus model)
        {
            var result = 0;
            using (var conn = DapperFactory.GetConnection())
            {
                conn.Open();
                var tran = conn.BeginTransaction();
                var fields = model.ToFields(removeFields: new List<string>
                {
                    nameof(model.Id),
                    nameof(model.CreateTime),
                    nameof(model.Status),
                    nameof(model.ImgUrls)
                });

                if (fields == null || fields.Count == 0)
                {
                    return false;
                }

                var fieldList = new List<string>();
                foreach (var field in fields)
                {
                    fieldList.Add($"{field}=@{field}");
                }

                model.ModifyTime = DateTime.Now;

                string sql = $"update [Campus] set {string.Join(",", fieldList)} where Id=@Id";

                var insertImg = @" INSERT INTO [CampusImg] ([Id],[Imgurl]) VALUES(@Id,@ImgUrl)";
                try
                {

                    result = await conn.ExecuteAsync(sql, model, tran);
                    if (model.ImgUrls != null)
                    {
                        foreach (var item in model.ImgUrls)
                        {
                            result = await conn.ExecuteAsync(insertImg, new { Id = model.Id, ImgUrl = item }, tran);
                        }
                    }
                    tran.Commit();
                }
                catch (SqlException ex)
                {
                    tran.Rollback();
                    return false;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return false;
                }
            }//end using
            return result > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "update [Campus] set Status=0,ModifyTime=GETDATE() where Id=@Id;";
                return await conn.ExecuteAsync(sql, new { Id = id }) > 0;
            }
        }

        public async Task<bool> DeleteImgAsync(int id, string imgUrl)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "delete from [CampusImg] where Id=@Id and ImgUrl=@ImgUrl;";
                return await conn.ExecuteAsync(sql, new { Id = id, ImgUrl = imgUrl }) > 0;
            }
        }
    }
}

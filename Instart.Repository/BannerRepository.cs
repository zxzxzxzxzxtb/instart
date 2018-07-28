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
        public Banner GetByIdAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select * from [Banner] where Id = @Id and Status=1;";
                return conn.QueryFirstOrDefault<Banner>(sql, new { Id = id });
            }
        }

        public PageModel<Banner> GetListAsync(int pageIndex, int pageSize, string title = null, int pos = 1, int type = -1)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                #region generate condition
                string where = "where Status=1";
                if (!string.IsNullOrEmpty(title))
                {
                    where += $" and Title like '%{title}%'";
                }
                if(pos > -1)
                {
                    where += $" and Pos = {pos}";
                }
                if(type > -1)
                {
                    where += $" and [Type]={type}";
                }
                #endregion

                string countSql = $"select count(1) from [Banner] {where};";
                int total = conn.ExecuteScalar<int>(countSql);
                if (total == 0)
                {
                    return new PageModel<Banner>();
                }

                string sql = $@"select * from ( select *, ROW_NUMBER() over (Order by Id desc) as RowNumber from [Banner] {where} ) as b where RowNumber between {((pageIndex - 1) * pageSize) + 1} and {pageIndex * pageSize};";
                var list = conn.Query<Banner>(sql);

                return new PageModel<Banner>
                {
                    Total = total,
                    Data = list?.ToList()
                };
            }
        }

        public List<Banner> GetListByPosAsync(EnumBannerPos pos = EnumBannerPos.Index)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select * from [Banner] where Pos = @Pos and Status=1;";
                var list = conn.Query<Banner>(sql, new { Pos = pos });
                return list?.ToList();
            }
        }

        public bool InsertAsync(Banner model)
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

                string sql = $"insert into [Banner] ({string.Join(",", fields)}) values ({string.Join(",", fields.Select(n => "@" + n))});";
                return conn.Execute(sql, model) > 0;
            }
        }

        public bool UpdateAsync(Banner model)
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
                foreach (var field in fields)
                {
                    fieldList.Add($"{field}=@{field}");
                }

                model.ModifyTime = DateTime.Now;

                string sql = $"update [Banner] set {string.Join(",", fieldList)} where Id=@Id;";
                return conn.Execute(sql, model) > 0;
            }
        }

        public bool DeleteAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "update [Banner] set Status=0,ModifyTime=GETDATE() where Id=@Id;";
                return conn.Execute(sql, new { Id = id }) > 0;
            }
        }

        public List<Banner> GetBannerListByPosAsync(EnumBannerPos pos, int topCount)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = $"select top {topCount} Id,Title,Type,ImageUrl,VideoUrl,Link from Banner where Pos=@Pos and IsShow=1 and Status=1 order by GroupIndex;";
                return (conn.Query<Banner>(sql, new { Pos = pos.ToInt32() }))?.ToList();
            }
        }

        public bool SetShowAsync(int id, bool isShow)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "update [Banner] set IsShow=@IsShow,ModifyTime=GETDATE() where Id=@Id;";
                return conn.Execute(sql, new { IsShow = isShow, Id = id }) > 0;
            }
        }
    }
}

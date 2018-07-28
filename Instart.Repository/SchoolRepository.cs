using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instart.Models;
using Dapper;
using System.Data.SqlClient;

namespace Instart.Repository
{
    public class SchoolRepository : ISchoolRepository
    {     
        public School GetByIdAsync(int id) {
            using (var conn = DapperFactory.GetConnection()) {
                string sql = "select * from [School] where Id = @Id and Status=1;";
                return conn.QueryFirstOrDefault<School>(sql, new { Id = id });
            }
        }

        public PageModel<School> GetListAsync(int pageIndex, int pageSize, string name = null) {
            using (var conn = DapperFactory.GetConnection()) {
                #region generate condition
                string where = "where Status=1";
                if (!string.IsNullOrEmpty(name)) {
                    where += $" and Name like '%{name}%'";
                }
                #endregion

                string countSql = $"select count(1) from [School] {where};";
                int total = conn.ExecuteScalar<int>(countSql);
                if (total == 0) {
                    return new PageModel<School>();
                }

                string sql = $@"select * from ( select *, ROW_NUMBER() over (Order by Id desc) as RowNumber from [School] {where} ) as b  
                                where RowNumber between {((pageIndex - 1) * pageSize) + 1} and {pageIndex * pageSize};";
                var list = conn.Query<School>(sql);

                return new PageModel<School> {
                    Total = total,
                    Data = list?.ToList()
                };
            }
        }

        public IEnumerable<School> GetAllAsync()
        {
            using (var conn = DapperFactory.GetConnection())
            {
                #region generate condition
                string where = "where Status=1";
                #endregion

                string sql = $@"select * from [School] {where};";
                return conn.Query<School>(sql);
            }
        }

        public bool InsertAsync(School model) {
            using (var conn = DapperFactory.GetConnection()) {
                var fields = model.ToFields(removeFields: new List<string> { nameof(model.Id), nameof(model.AcceptRate),
                    nameof(model.IsRecommend), nameof(model.IsHot) });
                if (fields == null || fields.Count == 0) {
                    return false;
                }

                model.CreateTime = DateTime.Now;
                model.ModifyTime = DateTime.Now;
                model.Status = 1;

                string sql = $"insert into [School] ({string.Join(",", fields)}) values ({string.Join(",", fields.Select(n => "@" + n))});";
                return conn.Execute(sql, model) > 0;
            }
        }

        public bool UpdateAsync(School model) {
            using (var conn = DapperFactory.GetConnection()) {
                var fields = model.ToFields(removeFields: new List<string>
                {
                    nameof(model.Id),
                    nameof(model.CreateTime),
                    nameof(model.Status),
                    nameof(model.AcceptRate),
                    nameof(model.IsRecommend),
                    nameof(model.IsHot)
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
                return conn.Execute(sql, model) > 0;
            }
        }

        public bool DeleteAsync(int id) {
            using (var conn = DapperFactory.GetConnection()) {
                string sql = "update [School] set Status=0,ModifyTime=GETDATE() where Id=@Id;";
                return conn.Execute(sql, new { Id = id }) > 0;
            }
        }

        public List<School> GetRecommendListAsync(int topCount)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = $"select top {topCount} Id,Name,NameEn,Difficult,Avatar,Country,Fee,Scholarship,LimitDate,Language from [School] where Status=1 and IsRecommend = 1 order by Id desc;";
                return (conn.Query<School>(sql, null))?.ToList();
            }
        }

        public bool SetRecommendAsync(int id, bool isRecommend)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = $"update [School] set IsRecommend=@IsRecommend where Id=@Id;";
                return conn.Execute(sql, new { IsRecommend =isRecommend, Id = id}) > 0;
            }
        }

        public List<School> GetHotListAsync(int topCount)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = $"select top {topCount} Id,Name,NameEn,Difficult,Avatar,Country,Fee,Scholarship,LimitDate,Language from [School] where Status=1 and IsHot = 1 order by Id desc;";
                return (conn.Query<School>(sql, null))?.ToList();
            }
        }

        public bool SetHotAsync(int id, bool isHot)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = $"update [School] set isHot=@isHot where Id=@Id;";
                return conn.Execute(sql, new { isHot = isHot, Id = id }) > 0;
            }
        }

        public PageModel<School> GetListAsync(int pageIndex, int pageSize, string name = null, int country = -1, int major = -1)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                #region generate condition
                string where = "where k.Status=1";
                if (!string.IsNullOrEmpty(name))
                {
                    where += $" and k.Name like '%{name}%'";
                }
                if (country != -1)
                {
                    where += $" and k.Country = {country}";
                }
                if (major != -1)
                {
                    where += $" and exists (select * from [SchoolMajor] as a where a.SchoolId = k.Id and a.MajorId = { major })";
                }
                #endregion

                string countSql = $"select count(1) from [School] as k {where};";
                int total = conn.ExecuteScalar<int>(countSql);
                if (total == 0)
                {
                    return new PageModel<School>
                    {
                        Total = 0,
                        Data = new List<School>()
                    };
                }

                string sql = $@"select * from ( select k.*, ROW_NUMBER() over (Order by k.Id desc) as RowNumber from [School] as k {where} ) as b  
                                where RowNumber between {((pageIndex - 1) * pageSize) + 1} and {pageIndex * pageSize};";
                var list = conn.Query<School>(sql);

                return new PageModel<School>
                {
                    Total = total,
                    Data = list?.ToList()
                };
            }
        }

        public IEnumerable<SchoolMajor> GetMajorsByIdAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = $"select s.SchoolId, s.MajorId, s.Introduce, m.Name as MajorName, m.Type from [SchoolMajor] as s left join [Major] as m on m.Id = s.MajorId where s.SchoolId={id};";
                return conn.Query<SchoolMajor>(sql); ;
            }
        }

        public bool SetMajors(int schoolId, string majorIds, string introduces)
        {
            var result = 0;
            using (var conn = DapperFactory.GetConnection())
            {
                conn.Open();
                var tran = conn.BeginTransaction();

                string sql = $"delete from [SchoolMajor] where SchoolId = @SchoolId; ";

                var insertImg = @" INSERT INTO [SchoolMajor] ([MajorId],[SchoolId],[Introduce]) VALUES(@MajorId,@SchoolId,@Introduce)";
                try
                {

                    result = conn.Execute(sql, new { SchoolId = schoolId }, tran);
                    if (!String.IsNullOrEmpty(majorIds))
                    {
                        string[] ids = majorIds.Split(',');
                        string[] introducearr = introduces.Split('|');
                        for (int i = 0; i < ids.Length; i++)
                        {
                            result = conn.Execute(insertImg, new { SchoolId = schoolId, MajorId = ids[i], Introduce = introducearr[i] }, tran);
                        }
                    }
                    tran.Commit();
                }
                catch (SqlException ex)
                {
                    result = 0;
                    tran.Rollback();
                    return false;
                }
                catch (Exception ex)
                {
                    result = 0;
                    tran.Rollback();
                    return false;
                }
            }//end using
            return result > 0;
        }
    }
}

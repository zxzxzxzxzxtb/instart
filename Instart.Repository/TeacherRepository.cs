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
    public class TeacherRepository : ITeacherRepository
    {
        public async Task<Teacher> GetByIdAsync(int id) {
            using (var conn = DapperFactory.GetConnection()) {
                string sql = "select * from [Teacher] where Id = @Id and Status=1;";
                return await conn.QueryFirstOrDefaultAsync<Teacher>(sql, new { Id = id });
            }
        }

        public async Task<PageModel<Teacher>> GetListAsync(int pageIndex, int pageSize, int division = -1, string name = null) {
            using (var conn = DapperFactory.GetConnection()) {
                #region generate condition
                string where = "where t.Status=1";
                if (!string.IsNullOrEmpty(name)) {
                    where += $" and t.Name like '%{name}%'";
                }
                if (division != -1)
                {
                    where += $" and t.DivisionId = {division}";
                }
                #endregion

                string countSql = $"select count(1) from [Teacher] as t {where};";
                int total = await conn.ExecuteScalarAsync<int>(countSql);
                if (total == 0) {
                    return new PageModel<Teacher>();
                }

                string sql = $@"select * from ( select t.*, d.Name as DivisionName, ROW_NUMBER() over (Order by t.Id desc) as RowNumber from [Teacher] as t
                    left join [Division] d on d.Id = t.DivisionId {where} ) as b 
                    where RowNumber between {((pageIndex - 1) * pageSize) + 1} and {pageIndex * pageSize};";
                var list = await conn.QueryAsync<Teacher>(sql);

                return new PageModel<Teacher> {
                    Total = total,
                    Data = list?.ToList()
                };
            }
        }

        public async Task<IEnumerable<Teacher>> GetAllAsync()
        {
            using (var conn = DapperFactory.GetConnection())
            {
                #region generate condition
                string where = "where Status=1";
                #endregion

                string sql = $@"select * from [Teacher] {where};";
                return await conn.QueryAsync<Teacher>(sql);
            }
        }

        public async Task<bool> InsertAsync(Teacher model) {
            using (var conn = DapperFactory.GetConnection()) {
                var fields = model.ToFields(removeFields: new List<string> { nameof(model.Id), nameof(model.DivisionName), nameof(model.DivisionNameEn),
                    nameof(model.SchoolName), nameof(model.SchoolNameEn), nameof(model.MajorName), nameof(model.MajorNameEn), nameof(model.IsSelected)});
                if (fields == null || fields.Count == 0) {
                    return false;
                }

                model.CreateTime = DateTime.Now;
                model.ModifyTime = DateTime.Now;
                model.Status = 1;

                string sql = $"insert into [Teacher] ({string.Join(",", fields)}) values ({string.Join(",", fields.Select(n => "@" + n))});";
                return await conn.ExecuteAsync(sql, model) > 0;
            }
        }

        public async Task<bool> UpdateAsync(Teacher model) {
            using (var conn = DapperFactory.GetConnection()) {
                var fields = model.ToFields(removeFields: new List<string>
                {
                    nameof(model.Id),
                    nameof(model.CreateTime),
                    nameof(model.Status),
                    nameof(model.DivisionName),
                    nameof(model.DivisionNameEn),
                    nameof(model.SchoolName),
                    nameof(model.SchoolNameEn),
                    nameof(model.MajorName),
                    nameof(model.MajorNameEn),
                    nameof(model.IsSelected)
                });

                if (fields == null || fields.Count == 0) {
                    return false;
                }

                var fieldList = new List<string>();
                foreach (var field in fields) {
                    fieldList.Add($"{field}=@{field}");
                }

                model.ModifyTime = DateTime.Now;

                string sql = $"update [Teacher] set {string.Join(",", fieldList)} where Id=@Id;";
                return await conn.ExecuteAsync(sql, model) > 0;
            }
        }

        public async Task<bool> DeleteAsync(int id) {
            using (var conn = DapperFactory.GetConnection()) {
                string sql = "update [Teacher] set Status=0,ModifyTime=GETDATE() where Id=@Id;";
                return await conn.ExecuteAsync(sql, new { Id = id }) > 0;
            }
        }

        public async Task<List<Teacher>> GetRecommendListAsync(int topCount)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = $@"select top {topCount} t.Id,t.Name,t.NameEn,t.Avatar,s.Name as SchoolName,s.NameEn as SchoolNameEn,
                                m.Name as MajorName,m.NameEn as MajorNameEn,d.Name as DivisionName,d.NameEn as DivisionNameEn from Teacher t
                                left join School s on t.SchoolId = s.Id
                                left join Major m on t.MajorId = m.Id
                                left join Division d on t.DivisionId = d.Id
                                where t.Status = 1 and t.IsRecommend = 1
                                order by t.Id desc;";
                return (await conn.QueryAsync<Teacher>(sql, null))?.ToList();
            }
        }

        public async Task<bool> SetRecommend(int id, bool isRecommend)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = $"update [Teacher] set IsRecommend=@IsRecommend where Id=@Id;";
                return await conn.ExecuteAsync(sql, new { IsRecommend = isRecommend, Id = id }) > 0;
            }
        }

        public async Task<IEnumerable<int>> GetCoursesByIdAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = $"select CourseId from [TeacherCourse] where TeacherId={id};";
                return await conn.QueryAsync<int>(sql); ;
            }
        }

        public async Task<bool> SetCourses(int teacherId, string courseIds)
        {
            var result = 0;
            using (var conn = DapperFactory.GetConnection())
            {
                conn.Open();
                var tran = conn.BeginTransaction();

                string sql = $"delete from [TeacherCourse] where TeacherId = @TeacherId; ";

                var insertImg = @" INSERT INTO [TeacherCourse] ([TeacherId],[CourseId]) VALUES(@TeacherId,@CourseId)";
                try
                {

                    result = await conn.ExecuteAsync(sql, new { TeacherId = teacherId }, tran);
                    if (!String.IsNullOrEmpty(courseIds))
                    {
                        string[] ids = courseIds.Split(',');
                        foreach (var item in ids)
                        {
                            result = await conn.ExecuteAsync(insertImg, new { TeacherId = teacherId, CourseId = item }, tran);
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

        public async Task<PageModel<Teacher>> GetListByDivsionAsync(int divisionId, int pageIndex, int pageSize)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string countSql = $"select count(1) from [Teacher] where DivisionId={divisionId} and Status=1;";

                int total = await conn.ExecuteScalarAsync<int>(countSql);
                if (total == 0)
                {
                    return new PageModel<Teacher>();
                }

                string sql = $@"select * from (select t.Id,t.Name,t.NameEn,t.Avatar,s.Name as SchoolName,s.NameEn as SchoolNameEn,
                                m.Name as MajorName,m.NameEn as MajorNameEn,d.Name as DivisionName,d.NameEn as DivisionNameEn, ROW_NUMBER() over (Order by t.Id desc) as RowNumber from Teacher t
                                left join School s on t.SchoolId = s.Id
                                left join Major m on t.MajorId = m.Id
                                left join Division d on t.DivisionId = d.Id
                                where t.DivisionId = @DivisionId and t.Status = 1 
                                order by t.Id desc) as b 
                                where RowNumber between {((pageIndex - 1) * pageSize) + 1} and {pageIndex * pageSize};";

                PageModel<Teacher> result = new PageModel<Teacher>
                {
                    Total = total,
                    Data = (await conn.QueryAsync<Teacher>(sql, new { DivisionId = divisionId }))?.ToList()
                };

                return result;
            }
        }
    }
}

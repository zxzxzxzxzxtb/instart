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
        public Teacher GetByIdAsync(int id) {
            using (var conn = DapperFactory.GetConnection()) {
                string sql = "select * from [Teacher] where Id = @Id and Status=1;";
                return conn.QueryFirstOrDefault<Teacher>(sql, new { Id = id });
            }
        }

        public PageModel<Teacher> GetListAsync(int pageIndex, int pageSize, int division = -1, string name = null) {
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
                int total = conn.ExecuteScalar<int>(countSql);
                if (total == 0) {
                    return new PageModel<Teacher>();
                }

                string sql = $@"select * from ( select t.*, d.Name as DivisionName, ROW_NUMBER() over (Order by t.Id desc) as RowNumber from [Teacher] as t
                    left join [Division] d on d.Id = t.DivisionId {where} ) as b 
                    where RowNumber between {((pageIndex - 1) * pageSize) + 1} and {pageIndex * pageSize};";
                var list = conn.Query<Teacher>(sql);

                return new PageModel<Teacher> {
                    Total = total,
                    Data = list?.ToList()
                };
            }
        }

        public IEnumerable<Teacher> GetAllAsync()
        {
            using (var conn = DapperFactory.GetConnection())
            {
                #region generate condition
                string where = "where Status=1";
                #endregion

                string sql = $@"select * from [Teacher] {where};";
                return conn.Query<Teacher>(sql);
            }
        }

        public bool InsertAsync(Teacher model) {
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
                return conn.Execute(sql, model) > 0;
            }
        }

        public bool UpdateAsync(Teacher model) {
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
                return conn.Execute(sql, model) > 0;
            }
        }

        public bool DeleteAsync(int id) {
            using (var conn = DapperFactory.GetConnection()) {
                string sql = "update [Teacher] set Status=0,ModifyTime=GETDATE() where Id=@Id;";
                return conn.Execute(sql, new { Id = id }) > 0;
            }
        }

        public List<Teacher> GetRecommendListAsync(int topCount)
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
                return (conn.Query<Teacher>(sql, null))?.ToList();
            }
        }

        public bool SetRecommend(int id, bool isRecommend)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = $"update [Teacher] set IsRecommend=@IsRecommend where Id=@Id;";
                return conn.Execute(sql, new { IsRecommend = isRecommend, Id = id }) > 0;
            }
        }

        public IEnumerable<int> GetCoursesByIdAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = $"select CourseId from [TeacherCourse] where TeacherId={id};";
                return conn.Query<int>(sql); ;
            }
        }

        public bool SetCourses(int teacherId, string courseIds)
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

                    result = conn.Execute(sql, new { TeacherId = teacherId }, tran);
                    if (!String.IsNullOrEmpty(courseIds))
                    {
                        string[] ids = courseIds.Split(',');
                        foreach (var item in ids)
                        {
                            result = conn.Execute(insertImg, new { TeacherId = teacherId, CourseId = item }, tran);
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

        public PageModel<Teacher> GetListByDivsionAsync(int divisionId, int pageIndex, int pageSize)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string countSql = $"select count(1) from [Teacher] where DivisionId={divisionId} and Status=1;";

                int total = conn.ExecuteScalar<int>(countSql);
                if (total == 0)
                {
                    return new PageModel<Teacher>();
                }

                string sql = $@"select * from (select TOP (100) PERCENT t.Id,t.Name,t.NameEn,t.Avatar,s.Name as SchoolName,s.NameEn as SchoolNameEn,
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
                    Data = (conn.Query<Teacher>(sql, new { DivisionId = divisionId }))?.ToList()
                };

                return result;
            }
        }
    }
}

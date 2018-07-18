using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instart.Models;
using Dapper;

namespace Instart.Repository
{
    public class StudentRepository : IStudentRepository
    {
        public async Task<Student> GetByIdAsync(int id) {
            using (var conn = DapperFactory.GetConnection()) {
                string sql = "select * from [Student] where Id = @Id and Status=1;";
                return await conn.QueryFirstOrDefaultAsync<Student>(sql, new { Id = id });
            }
        }

        public async Task<PageModel<Student>> GetListAsync(int pageIndex, int pageSize, string name = null) {
            using (var conn = DapperFactory.GetConnection()) {
                #region generate condition
                string where = "where a.Status=1";
                if (!string.IsNullOrEmpty(name)) {
                    where += $" and a.Name like '%{name}%'";
                }
                #endregion

                string countSql = $"select count(1) from [Student] as a {where};";
                int total = await conn.ExecuteScalarAsync<int>(countSql);
                if (total == 0) {
                    return new PageModel<Student>();
                }

                string sql = $@"select * from (
                     select a.*, b.Name as MajorName, b.NameEn as MajorNameEn, c.Name as TeacherName, c.NameEn as TeacherNameEn, 
                     e.Name as SchoolName, e.NameEn as SchoolNameEn, ROW_NUMBER() over (Order by a.Id desc) as RowNumber from [Student] as a
                     left join [Major] as b on b.Id = a.MajorId 
                     left join [Teacher] as c on c.Id = a.TeacherId 
                     left join [School] as e on e.Id = a.SchoolId {where}
                     ) as d
                     where RowNumber between {((pageIndex - 1) * pageSize) + 1} and {pageIndex * pageSize};";
                var list = await conn.QueryAsync<Student>(sql.Trim());

                return new PageModel<Student> {
                    Total = total,
                    Data = list?.ToList()
                };
            }
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            using (var conn = DapperFactory.GetConnection())
            {
                #region generate condition
                string where = "where Status=1";
                #endregion

                string sql = $@"select * from [Student] {where};";
                return await conn.QueryAsync<Student>(sql);
            }
        }

        public async Task<IEnumerable<Student>> GetStarStudentsAsync()
        {
            using (var conn = DapperFactory.GetConnection())
            {
                #region generate condition
                string where = "where Status=1 and ImgUrl is not null and VideoUrl is not null";
                #endregion

                string sql = $@"select * from [Student] {where};";
                return await conn.QueryAsync<Student>(sql);
            }
        }

        public async Task<bool> InsertAsync(Student model) {
            using (var conn = DapperFactory.GetConnection()) {
                var fields = model.ToFields(removeFields: new List<string> { nameof(model.Id), nameof(model.SchoolName), nameof(model.SchoolNameEn),
                    nameof(model.MajorName), nameof(model.MajorNameEn), nameof(model.TeacherName), nameof(model.TeacherNameEn) });
                if (fields == null || fields.Count == 0) {
                    return false;
                }

                model.CreateTime = DateTime.Now;
                model.ModifyTime = DateTime.Now;
                model.Status = 1;

                string sql = $"insert into [Student] ({string.Join(",", fields)}) values ({string.Join(",", fields.Select(n => "@" + n))});";
                return await conn.ExecuteAsync(sql, model) > 0;
            }
        }

        public async Task<bool> UpdateAsync(Student model) {
            using (var conn = DapperFactory.GetConnection()) {
                var fields = model.ToFields(removeFields: new List<string>
                {
                    nameof(model.Id),
                    nameof(model.CreateTime),
                    nameof(model.Status),
                    nameof(model.MajorName),
                    nameof(model.MajorNameEn),
                    nameof(model.TeacherName),
                    nameof(model.TeacherNameEn),
                    nameof(model.SchoolName),
                    nameof(model.SchoolNameEn)
                });

                if (fields == null || fields.Count == 0) {
                    return false;
                }

                var fieldList = new List<string>();
                foreach (var field in fields) {
                    fieldList.Add($"{field}=@{field}");
                }

                model.ModifyTime = DateTime.Now;

                string sql = $"update [Student] set {string.Join(",", fieldList)} where Id=@Id;";
                return await conn.ExecuteAsync(sql, model) > 0;
            }
        }

        public async Task<bool> DeleteAsync(int id) {
            using (var conn = DapperFactory.GetConnection()) {
                string sql = "update [Student] set Status=0,ModifyTime=GETDATE() where Id=@Id;";
                return await conn.ExecuteAsync(sql, new { Id = id }) > 0;
            }
        }

        public async Task<List<Student>> GetRecommendListAsync(int topCount)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = $@"select top {topCount} t.*, b.Name as MajorName, b.NameEn as MajorNameEn, c.Name as TeacherName, 
                     c.NameEn as TeacherNameEn, e.Name as SchoolName, e.NameEn as SchoolNameEn from Student t 
                     left join [Major] as b on b.Id = t.MajorId 
                     left join [Teacher] as c on c.Id = t.TeacherId 
                     left join [School] as e on e.Id = t.SchoolId
                     where t.Status=1 and t.IsRecommend=1
                     order by t.Id Desc;";
                return (await conn.QueryAsync<Student>(sql, null))?.ToList();
            }
        }

        public async Task<bool> SetRecommend(int id, bool isRecommend)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = $"update [Student] set IsRecommend=@IsRecommend where Id=@Id;";
                return await conn.ExecuteAsync(sql, new { IsRecommend = isRecommend, Id = id }) > 0;
            }
        }
    }
}

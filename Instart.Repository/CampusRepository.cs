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
        public Campus GetByIdAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select * from [Campus] where Id = @Id and Status=1;";
                return conn.QueryFirstOrDefault<Campus>(sql, new { Id = id });
            }
        }

        public PageModel<Campus> GetListAsync(int pageIndex, int pageSize, string name = null)
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
                int total = conn.ExecuteScalar<int>(countSql);
                if (total == 0)
                {
                    return new PageModel<Campus>();
                }

                string sql = $@"select * from (
                             select *, ROW_NUMBER() over (Order by Id desc) as RowNumber from [Campus] {where}
                             ) as b
                             where RowNumber between {((pageIndex - 1) * pageSize) + 1} and {pageIndex * pageSize};";
                var list = conn.Query<Campus>(sql);

                return new PageModel<Campus>
                {
                    Total = total,
                    Data = list?.ToList()
                };
            }
        }

        public IEnumerable<Campus> GetAllAsync()
        {
            using (var conn = DapperFactory.GetConnection())
            {
                #region generate condition
                string where = "where Status=1";
                #endregion

                string sql = $@"select * from [Campus] {where};";
                return conn.Query<Campus>(sql);
            }
        }

        public bool InsertAsync(Campus model)
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

                string sql = $"insert into [Campus] ({string.Join(",", fields)}) values ({string.Join(",", fields.Select(n => "@" + n))});";
                return conn.Execute(sql, model) > 0;
            }
        }

        public bool UpdateAsync(Campus model)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                List<string> removeFields = new List<string>
                {
                    nameof(model.Id),
                    nameof(model.CreateTime),
                    nameof(model.Status)
                };
                var fields = model.ToFields(removeFields: removeFields);

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

                string sql = $"update [Campus] set {string.Join(",", fieldList)} where Id=@Id;";
                return conn.Execute(sql, model) > 0;
            }
        }

        public bool DeleteAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "update [Campus] set Status=0,ModifyTime=GETDATE() where Id=@Id;";
                return conn.Execute(sql, new { Id = id }) > 0;
            }
        }

        public IEnumerable<CampusImg> GetImgsByCampusIdAsync(int campusId)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                #region generate condition
                string where = $"where Status=1 and CampusId = {campusId}";
                #endregion

                string sql = $@"select * from [CampusImg] {where};";
                return conn.Query<CampusImg>(sql);
            }
        }

        public bool InsertImgAsync(CampusImg model)
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

                string sql = $"insert into [CampusImg] ({string.Join(",", fields)}) values ({string.Join(",", fields.Select(n => "@" + n))});";
                return conn.Execute(sql, model) > 0;
            }
        }

        public bool DeleteImgAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "update [CampusImg] set Status=0,ModifyTime=GETDATE() where Id=@Id;";
                return conn.Execute(sql, new { Id = id }) > 0;
            }
        }
    }
}

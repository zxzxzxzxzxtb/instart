using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instart.Models;
using Dapper;

namespace Instart.Repository
{
    public class UserRepository : IUserRepository
    {
        public User GetByIdAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select * from [User] where Id = @Id and Status=1;";
                var list = conn.Query<User>(sql, new { Id = id });
                return list != null ? list.FirstOrDefault() : null;
            }
        }

        public User GetById(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select * from [User] where Id = @Id and Status=1;";
                var list = conn.Query<User>(sql, new { Id = id });
                return list != null ? list.FirstOrDefault() : null;
            }
        }

        public User GetByNameAsync(string name)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select * from [User] where UserName = @name and Status=1;";
                return conn.QueryFirstOrDefault<User>(sql, new { name = name });
            }
        }

        public PageModel<User> GetListAsync(int pageIndex, int pageSize)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string countSql = "select count(1) from [User] where Status=1;";
                int total = conn.ExecuteScalar<int>(countSql);
                if (total == 0)
                {
                    return new PageModel<User>();
                }
                
                string sql = string.Format(@"select * from (select *, ROW_NUMBER() over (Order by Id desc) as RowNumber from [User] where Status=1) as b where RowNumber between {0} and {1};",((pageIndex - 1) * pageSize) + 1,pageIndex * pageSize);
                var list = conn.Query<User>(sql);

                return new PageModel<User>
                {
                    Total = total,
                    Data = list != null ? list.ToList() : null
                };
            }
        }

        public bool InsertAsync(User model)
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

                string sql = string.Format("insert into [User] ({0}) values ({1});",string.Join(",", fields),string.Join(",", fields.Select(n => "@" + n)));
                return conn.Execute(sql, model) > 0;
            }
        }

        public bool UpdateAsync(User model)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                var fields = model.ToFields(removeFields: new List<string>
                {
                    "Id",
                    "CreateTime",
                    "Status",
                    "Password"
                });

                if (fields == null || fields.Count == 0)
                {
                    return false;
                }

                var fieldList = new List<string>();
                foreach (var field in fields)
                {
                    fieldList.Add(string.Format("{0}=@{0}",field));
                }

                model.ModifyTime = DateTime.Now;

                string sql = string.Format("update [User] set {0} where Id=@Id;",string.Join(",", fieldList));
                return conn.Execute(sql, model) > 0;
            }
        }

        public bool UpdatePasswordAsync(int id, string password)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "update [User] set Password=@Password,ModifyTime=GETDATE() where Id=@Id;";
                return conn.Execute(sql, new { Id = id, Password = password }) > 0;
            }
        }

        public bool DeleteAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "update [User] set Status=0,ModifyTime=GETDATE() where Id=@Id;";
                return conn.Execute(sql, new { Id = id }) > 0;
            }
        }
    }
}

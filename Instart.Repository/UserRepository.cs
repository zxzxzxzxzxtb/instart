using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instart.Repository;
using Instart.Models;
using System.Data.SqlClient;
using Dapper;
using System.Linq.Expressions;

namespace Instart.Repository
{
    public class UserRepository : IUserRepository
    {
        public Task<User> GetByIdAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select * from `User` where Id = @Id and Status=1;";
                return conn.QuerySingleAsync<User>(sql, new { Id = id });
            }
        }

        public async Task<User> GetByNameAsync(string name)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select * from `User` where Name = @name and Status=1;";
                return await conn.QuerySingleAsync<User>(sql, new { name = name });
            }
        }

        public async Task<PageModel<User>> GetListAsync(int pageIndex, int pageSize)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string countSql = "select count(1) from `User` where and Status=1;";
                int total = await conn.ExecuteScalarAsync<int>(countSql);
                if (total == 0)
                {
                    return new PageModel<User>();
                }

                string sql = $"select * from `User` where Status=1 limit {(pageIndex - 1) * pageIndex},{pageSize};";
                var list = await conn.QueryAsync<User>(sql);

                return new PageModel<User>
                {
                    Total = total,
                    Data = list?.ToList()
                };
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "update `User` set Status=0 where Id=@Id;";
                var result = await conn.ExecuteAsync(sql, new { Id = id });
                return result > 0;
            }
        }

        public async Task<bool> UpdatePasswordAsync(int id, string password)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "update `User` set Password=@Password where Id=@Id;";
                var result = await conn.ExecuteAsync(sql, new { Id = id, Password = password });
                return result > 0;
            }
        }
    }
}

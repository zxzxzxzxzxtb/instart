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
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public Task<User> GetByIdAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select * from user where Id = @Id";
                return conn.QuerySingleAsync<User>(sql, new { Id = id });
            }
        }

        public async Task<User> GetByNameAsync(string name)
        {
            var result = await base.GetAsync(p => p.Account.Equals(name), p => p);
            return result?.FirstOrDefault();
        }

        public async Task<PageModel<User>> GetListAsync(int pageIndex, int pageSize)
        {
            var result = new PageModel<User>();
            Expression<Func<User, User>> selector = p => new User
            {
                Id = p.Id,
                UserName = p.UserName,
                Role = p.Role,
                CreateTime = p.CreateTime
            };
            result.Total = await base.CountAsync(p => true);
            result.Data = await base.GetAsync(p => true, selector, pageIndex, pageSize, p => p.CreateTime, false);
            return result;
        }
    }
}

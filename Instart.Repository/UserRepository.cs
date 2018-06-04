using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instart.Repository;
using Instart.Models;
using System.Data.SqlClient;
using Dapper;
using Instart.Common;

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

        public async Task<User> GetByNameAsync(string name) {
            var result = await base.GetAsync(n => n.UserName.Equals(name));
            return result?.FirstOrDefault();
        }

        public async Task<PageModel<User>> GetUserListAsync(int pageIndex, int pageSize) {
            var result = new PageModel<User>();
            result.Total = await base.CountAsync(null);
            result.Data = await base.GetAsync(p => true, pageIndex, pageSize, p => p.CreateTime, false);
            return result;
        }
    }
}

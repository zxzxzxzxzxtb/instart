using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instart.Repository;
using Instart.Models;

namespace Instart.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(InstartDbContext context) : base(context) {

        }

        public User GetByName(string name) {
            return this.Get(n => n.UserName.Equals(name)).FirstOrDefault();
        }

        public IEnumerable<User> GetUsers(int pageIndex, int pageSize, out int total) {
            total = this.DbSet.Count();
            return this.Get(p => true, pageIndex, pageSize, p => p.CreateTime, false);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instart.Repository.Models;
using Instart.Repository;

namespace Instart.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(InstartDbContext context) : base(context) {

        }

        public User GetUser(string name, string password) {
            return this.Get(n => n.UserName.Equals(name) && n.Password.Equals(password)).FirstOrDefault();
        }
    }
}

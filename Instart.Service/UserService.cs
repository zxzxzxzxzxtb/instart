using Instart.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instart.Models;

namespace Instart.Service
{
    public class UserService : ServiceBase, IUserService
    {
        public IUserRepository UserRepository { get; private set; }

        public UserService(IUserRepository repository) {
            this.UserRepository = repository;
            base.AddDisposableObject(repository);
        }

        public User GetByName(string name) {
            if (string.IsNullOrEmpty(name)) {
                return null;
            }
            return this.UserRepository.GetByName(name);
        }

        public IEnumerable<User> GetUsers(int pageIndex, int pageSize, out int total) {
            pageIndex = Math.Max(pageIndex, 1);
            return this.UserRepository.GetUsers(pageIndex, pageSize, out total);
        }
    }
}

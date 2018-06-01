using Instart.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instart.Models;
using Instart.Repository.Base;

namespace Instart.Service
{
    public class UserService : ServiceBase, IUserService
    {
        IUserRepository _userRepository = AutofacRepository.Resolve<IUserRepository>();

        public UserService() {
            base.AddDisposableObject(_userRepository);
        }

        public User GetByName(string name) {
            if (string.IsNullOrEmpty(name)) {
                return null;
            }
            return _userRepository.GetByName(name);
        }

        public IEnumerable<User> GetUsers(int pageIndex, int pageSize, out int total) {
            pageIndex = Math.Max(pageIndex, 1);
            return _userRepository.GetUsers(pageIndex, pageSize, out total);
        }
    }
}

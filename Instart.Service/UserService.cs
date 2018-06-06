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
        IUserRepository _userRepository = AutofacRepository.Resolve<IUserRepository>();

        public UserService() {
            base.AddDisposableObject(_userRepository);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                return null;
            }
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<User> GetByNameAsync(string name) {
            if (string.IsNullOrEmpty(name)) {
                return null;
            }
            return await _userRepository.GetByNameAsync(name);
        }

        public async Task<PageModel<User>> GetListAsync(int pageIndex, int pageSize) {
            return await _userRepository.GetListAsync(pageIndex, pageSize);
        }
    }
}

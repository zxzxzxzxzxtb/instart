using Instart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Service
{
    public interface IUserService
    {
        Task<User> GetByIdAsync(int id);

        Task<User> GetByNameAsync(string name);

        Task<PageModel<User>> GetListAsync(int pageIndex, int pageSize);

        Task<bool> DeleteAsync(int id);

        Task<bool> UpdatePasswordAsync(int id, string password);
    }
}

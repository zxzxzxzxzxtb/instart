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
         User GetByIdAsync(int id);

        User GetById(int id);

         User GetByNameAsync(string name);

         PageModel<User> GetListAsync(int pageIndex, int pageSize);

         bool DeleteAsync(int id);

         bool UpdatePasswordAsync(int id, string password);

         bool InsertAsync(User model);

         bool UpdateAsync(User model);
    }
}

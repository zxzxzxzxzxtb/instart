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
        User GetByName(string name);

        IEnumerable<User> GetUsers(int pageIndex, int pageSize, out int total);
    }
}

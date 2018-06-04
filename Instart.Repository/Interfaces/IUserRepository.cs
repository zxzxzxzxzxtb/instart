﻿using Instart.Common;
using Instart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Repository
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(int id);

        Task<User> GetByNameAsync(string name);

        Task<PageModel<User>> GetUserListAsync(int pageIndex, int pageSize);
    }
}
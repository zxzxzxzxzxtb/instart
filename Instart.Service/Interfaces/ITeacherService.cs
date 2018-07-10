﻿using Instart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Service
{
    public interface ITeacherService
    {
        Task<Teacher> GetByIdAsync(int id);

        Task<PageModel<Teacher>> GetListAsync(int pageIndex, int pageSize, string name = null);

        Task<bool> InsertAsync(Teacher model);

        Task<bool> UpdateAsync(Teacher model);

        Task<bool> DeleteAsync(int id);
    }
}
using Instart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Service
{
    public interface IProgramService
    {
        Program GetByIdAsync(int id);

        PageModel<Program> GetListAsync(int pageIndex, int pageSize, int type = -1, string name = null);

        IEnumerable<Program> GetAllAsync();

        bool InsertAsync(Program model);

        bool UpdateAsync(Program model);

        bool DeleteAsync(int id);

        IEnumerable<Program> GetListByTypeAsync(int type);
    }
}

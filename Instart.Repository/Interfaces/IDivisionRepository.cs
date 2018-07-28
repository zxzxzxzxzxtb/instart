using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instart.Models;
using Instart.Models.Enums;

namespace Instart.Repository
{
    public interface IDivisionRepository
    {
        Division GetByIdAsync(int id);

        PageModel<Division> GetListAsync(int pageIndex, int pageSize, string name = null);

        IEnumerable<Division> GetAllAsync();

        bool InsertAsync(Division model);

        bool UpdateAsync(Division model);

        bool DeleteAsync(int id);
    }
}

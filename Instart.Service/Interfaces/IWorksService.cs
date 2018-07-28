using Instart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Service
{
    public interface IWorksService
    {
         Works GetByIdAsync(int id);

         PageModel<Works> GetListAsync(int pageIndex, int pageSize, string name = null);

         bool InsertAsync(Works model);

         bool UpdateAsync(Works model);

         bool DeleteAsync(int id);

         List<Works> GetListByMajorIdAsync(int majorId, int topCount);
    }
}

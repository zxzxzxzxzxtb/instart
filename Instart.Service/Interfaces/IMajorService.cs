using Instart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Service
{
    public interface IMajorService
    {
         Major GetByIdAsync(int id);

         PageModel<Major> GetListAsync(int pageIndex, int pageSize, int division = -1, string name = null);

         IEnumerable<Major> GetAllAsync();

         bool InsertAsync(Major model);

         bool UpdateAsync(Major model);

         bool DeleteAsync(int id);

         PageModel<Major> GetListByDivsionAsync(int divisionId, int pageIndex, int pageSize);
    }
}

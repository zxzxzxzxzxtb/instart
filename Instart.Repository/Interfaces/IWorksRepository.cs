using Instart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Repository
{
    public interface IWorksRepository
    {
        Task<Works> GetByIdAsync(int id);        

        Task<PageModel<Works>> GetListAsync(int pageIndex, int pageSize, string name = null);

        Task<bool> InsertAsync(Works model);

        Task<bool> UpdateAsync(Works model);

        Task<bool> DeleteAsync(int id);

        Task<List<Works>> GetListByMajorIdAsync(int majorId, int topCount);
    }
}

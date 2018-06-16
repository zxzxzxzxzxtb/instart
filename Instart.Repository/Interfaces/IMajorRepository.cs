using Instart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Repository
{
    public interface IMajorRepository
    {
        Task<Major> GetByIdAsync(int id);        

        Task<PageModel<Major>> GetListAsync(int pageIndex, int pageSize, string name = null);

        Task<bool> InsertAsync(Major model);

        Task<bool> UpdateAsync(Major model);

        Task<bool> DeleteAsync(int id);
    }
}

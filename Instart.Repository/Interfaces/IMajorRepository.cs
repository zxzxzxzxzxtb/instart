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

        Task<PageModel<Major>> GetListAsync(int pageIndex, int pageSize, int division = -1, string name = null);

        Task<IEnumerable<Major>> GetAllAsync();

        Task<bool> InsertAsync(Major model);

        Task<bool> UpdateAsync(Major model);

        Task<bool> DeleteAsync(int id);

        Task<PageModel<Major>> GetListByDivsionAsync(int divisionId, int pageIndex, int pageSize);
    }
}

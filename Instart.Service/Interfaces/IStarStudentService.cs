using Instart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Service
{
    public interface IStarStudentService
    {
        Task<StarStudent> GetByIdAsync(int id);

        Task<PageModel<StarStudent>> GetListAsync(int pageIndex, int pageSize, string name = null);

        Task<IEnumerable<StarStudent>> GetAllAsync();

        Task<bool> InsertAsync(StarStudent model);

        Task<bool> UpdateAsync(StarStudent model);

        Task<bool> DeleteAsync(int id);
    }
}

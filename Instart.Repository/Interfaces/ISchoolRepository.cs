using Instart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Repository
{
    public interface ISchoolRepository
    {
        Task<School> GetByIdAsync(int id);        

        Task<PageModel<School>> GetListAsync(int pageIndex, int pageSize, string name = null);

        Task<bool> InsertAsync(School model);

        Task<bool> UpdateAsync(School model);

        Task<bool> DeleteAsync(int id);
    }
}

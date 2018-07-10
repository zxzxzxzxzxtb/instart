using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instart.Models;

namespace Instart.Service
{
    public interface ICampusService
    {
        Task<Campus> GetByIdAsync(int id);

        Task<PageModel<Campus>> GetListAsync(int pageIndex, int pageSize, string name = null);

        Task<IEnumerable<Campus>> GetAllAsync();

        Task<bool> InsertAsync(Campus model);

        Task<bool> UpdateAsync(Campus model);

        Task<bool> DeleteAsync(int id);

        Task<bool> DeleteImgAsync(int id, string imgUrl);
    }
}

using Instart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Service
{
    public interface IPartnerService
    {
        Task<Partner> GetByIdAsync(int id);

        Task<PageModel<Partner>> GetListAsync(int pageIndex, int pageSize, string name = null);

        Task<bool> InsertAsync(Partner model);

        Task<bool> UpdateAsync(Partner model);

        Task<bool> DeleteAsync(int id);

        Task<List<Partner>> GetRecommendListAsync(int topCount);
    }
}

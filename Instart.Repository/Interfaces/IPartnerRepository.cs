using Instart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Repository
{
    public interface IPartnerRepository
    {
        Partner GetByIdAsync(int id);

        PageModel<Partner> GetListAsync(int pageIndex, int pageSize, string name = null);

        bool InsertAsync(Partner model);

        bool UpdateAsync(Partner model);

        bool DeleteAsync(int id);

        List<Partner> GetRecommendListAsync(int topCount);
    }
}

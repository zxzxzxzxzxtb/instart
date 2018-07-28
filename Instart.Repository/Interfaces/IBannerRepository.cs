using Instart.Models;
using Instart.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Repository
{
    public interface IBannerRepository
    {
        Banner GetByIdAsync(int id);

        List<Banner> GetListByPosAsync(EnumBannerPos pos = EnumBannerPos.Index);       

        PageModel<Banner> GetListAsync(int pageIndex, int pageSize, string title = null, int pos = 1, int type = -1);

        bool InsertAsync(Banner model);

        bool UpdateAsync(Banner model);

        bool DeleteAsync(int id);

        List<Banner> GetBannerListByPosAsync(EnumBannerPos pos, int topCount);

        bool SetShowAsync(int id, bool isShow);
    }
}

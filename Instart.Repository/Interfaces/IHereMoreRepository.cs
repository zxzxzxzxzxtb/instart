using Instart.Models;
using Instart.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Repository
{
    public interface IHereMoreRepository
    {
        PageModel<HereMore> GetListAsync(int pageIndex, int pageSize, string name, EnumAccept accept);

        bool SetAcceptAsync(int id);

        bool InsertAsync(HereMore model);

        List<HereMore> GetTopListAsync(int topCount);
    }
}

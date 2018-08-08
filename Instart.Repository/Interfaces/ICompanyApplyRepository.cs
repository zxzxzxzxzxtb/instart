using Instart.Models;
using Instart.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Repository
{
    public interface ICompanyApplyRepository
    {
        PageModel<CompanyApply> GetListAsync(int pageIndex, int pageSize, string name, EnumAccept accept);

        bool SetAcceptAsync(int id);

        bool InsertAsync(CompanyApply model);

        List<CompanyApply> GetTopListAsync(int topCount);
    }
}

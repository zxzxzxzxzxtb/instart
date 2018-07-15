using Instart.Models;
using Instart.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Service
{
    public interface ISchoolApplyService
    {
        Task<PageModel<SchoolApply>> GetListAsync(int pageIndex, int pageSize, string schoolName, EnumAccept accept);

        Task<List<string>> GetApplySchoolNameListAsync();

        Task<bool> SetAcceptAsync(int id);

        Task<bool> InsertAsync(SchoolApply model);
    }
}

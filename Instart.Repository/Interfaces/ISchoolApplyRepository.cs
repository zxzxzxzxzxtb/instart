using Instart.Models;
using Instart.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Repository
{
    public interface ISchoolApplyRepository
    {
        PageModel<SchoolApply> GetListAsync(int pageIndex, int pageSize, string schoolName, EnumAccept accept);

        List<string> GetApplySchoolNameListAsync();

        bool SetAcceptAsync(int id);

        bool InsertAsync(SchoolApply model);

        List<SchoolApply> GetTopListAsync(int topCount);
    }
}

using Instart.Models;
using Instart.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Repository
{
    public interface ICourseApplyRepository
    {
        PageModel<CourseApply> GetListAsync(int pageIndex, int pageSize, string courseName, EnumAccept accept);

        List<string> GetApplyCourseNameListAsync();

        bool SetAcceptAsync(int id);

        bool InsertAsync(CourseApply model);

        List<CourseApply> GetTopListAsync(int topCount);

    }
}

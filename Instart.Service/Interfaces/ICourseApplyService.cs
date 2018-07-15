using Instart.Models;
using Instart.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Service
{
    public interface ICourseApplyService
    {
        Task<PageModel<Models.CourseApply>> GetListAsync(int pageIndex, int pageSize, string courseName, EnumAccept accept);

        Task<List<string>> GetApplyCourseNameListAsync();

        Task<bool> SetAcceptAsync(int id);

        Task<bool> InsertAsync(CourseApply model);

        Task<List<CourseApply>> GetTopListAsync(int topCount);
    }
}

using Instart.Models;
using Instart.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Service
{
    public interface ICourseOrderService
    {
        PageModel<Models.CourseOrder> GetListAsync(int pageIndex, int pageSize, string courseName, EnumAccept accept);

        bool SetAcceptAsync(int id);

        bool InsertAsync(CourseOrder model);

        List<CourseOrder> GetTopListAsync(int topCount);
    }
}

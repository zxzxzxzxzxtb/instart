using Instart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Repository
{
    public interface ILogRepository
    {
        PageModel<Log> GetListAsync(int pageIndex, int pageSize, int userId, string title);

        bool Insert(Log model);

        List<Log> GetTopListAsync(int topCount);
    }
}

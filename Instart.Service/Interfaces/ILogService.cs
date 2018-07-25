using Instart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Service
{
    public interface ILogService
    {
        Task<PageModel<Log>> GetListAsync(int pageIndex, int pageSize, string title, int userId, int type);

        Task<bool> InsertAsync(Log model);

        Task<List<Log>> GetTopListAsync(int topCount);
    }
}

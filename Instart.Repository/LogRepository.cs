using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instart.Models;

namespace Instart.Repository
{
    public class LogRepository : ILogRepository
    {
        public async Task<PageModel<Log>> GetListAsync(int pageIndex, int pageSize, string title)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Log>> GetTopListAsync(int topCount)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertAsync(Log model)
        {
            throw new NotImplementedException();
        }
    }
}

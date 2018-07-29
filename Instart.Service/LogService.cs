using Instart.Models;
using Instart.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Service
{
    public class LogService : ServiceBase, ILogService
    {
        ILogRepository _logRepository = AutofacRepository.Resolve<ILogRepository>();

        public LogService()
        {
            base.AddDisposableObject(_logRepository);
        }

        public PageModel<Log> GetListAsync(int pageIndex, int pageSize, int userId, string title)
        {
            return _logRepository.GetListAsync(pageIndex, pageSize, userId, title);
        }

        public List<Log> GetTopListAsync(int topCount)
        {
            if (topCount == 0)
            {
                return null;
            }

            return _logRepository.GetTopListAsync(topCount);
        }

        public bool Insert(Log model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model不能为null");
            }

            if (string.IsNullOrEmpty(model.Title))
            {
                throw new ArgumentNullException("Title不能为null");
            }

            return _logRepository.Insert(model);
        }
    }
}

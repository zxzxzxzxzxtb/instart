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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instart.Models;
using Instart.Repository;

namespace Instart.Service
{
    public class StatisticsService : ServiceBase, IStatisticsService
    {
        IStatisticsRepository _statisticsRepository = AutofacRepository.Resolve<IStatisticsRepository>();

        public StatisticsService()
        {
            base.AddDisposableObject(_statisticsRepository);
        }

        public  Statistics GetAsync()
        {
            return _statisticsRepository.GetAsync(); 
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instart.Models;
using Instart.Repository;

namespace Instart.Service
{
    public class RecruitService : ServiceBase, IRecruitService
    {
        IRecruitRepository _recruitRepository = AutofacRepository.Resolve<IRecruitRepository>();

        public RecruitService()
        {
            base.AddDisposableObject(_recruitRepository);
        }

        public int GetCountAsync()
        {
            return _recruitRepository.GetCountAsync();
        }

        public Recruit GetInfoAsync()
        {
            return _recruitRepository.GetInfoAsync();
        }

        public bool InsertAsync(Recruit model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return _recruitRepository.InsertAsync(model);
        }

        public bool UpdateAsync(Recruit model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return _recruitRepository.UpdateAsync(model);
        }
    }
}

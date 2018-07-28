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

        public async Task<int> GetCountAsync()
        {
            return await _recruitRepository.GetCountAsync();
        }

        public async Task<Recruit> GetInfoAsync()
        {
            return await _recruitRepository.GetInfoAsync();
        }

        public async Task<bool> InsertAsync(Recruit model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return await _recruitRepository.InsertAsync(model);
        }

        public async Task<bool> UpdateAsync(Recruit model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return await _recruitRepository.UpdateAsync(model);
        }
    }
}

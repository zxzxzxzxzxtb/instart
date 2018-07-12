using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instart.Models;
using Instart.Repository;

namespace Instart.Service
{
    public class PartnerService : ServiceBase, IPartnerService
    {
        IPartnerRepository _partnerRepository = AutofacRepository.Resolve<IPartnerRepository>();

        public PartnerService()
        {
            base.AddDisposableObject(_partnerRepository);
        }

        public async Task<Partner> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id));
            }

            return await _partnerRepository.GetByIdAsync(id);
        }

        public async Task<PageModel<Partner>> GetListAsync(int pageIndex, int pageSize, string name = null)
        {
            return await _partnerRepository.GetListAsync(pageIndex, pageSize, name);
        }

        public async Task<bool> InsertAsync(Partner model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrEmpty(model.Name))
            {
                throw new ArgumentNullException(nameof(model.Name));
            }

            return await _partnerRepository.InsertAsync(model);
        }

        public async Task<bool> UpdateAsync(Partner model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrEmpty(model.Name))
            {
                throw new ArgumentNullException(nameof(model.Name));
            }

            if (model.Id <= 0)
            {
                throw new ArgumentException(nameof(model.Id));
            }

            return await _partnerRepository.UpdateAsync(model);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id));
            }

            return await _partnerRepository.DeleteAsync(id);
        }

        public async Task<List<Partner>> GetRecommendListAsync(int topCount)
        {
            if (topCount == 0)
            {
                return null;
            }

            return await _partnerRepository.GetRecommendListAsync(topCount);
        }
    }
}

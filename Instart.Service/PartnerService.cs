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

        public Partner GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id));
            }

            return _partnerRepository.GetByIdAsync(id);
        }

        public PageModel<Partner> GetListAsync(int pageIndex, int pageSize, string name = null)
        {
            return _partnerRepository.GetListAsync(pageIndex, pageSize, name);
        }

        public bool InsertAsync(Partner model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrEmpty(model.Name))
            {
                throw new ArgumentNullException(nameof(model.Name));
            }

            return _partnerRepository.InsertAsync(model);
        }

        public bool UpdateAsync(Partner model)
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

            return _partnerRepository.UpdateAsync(model);
        }

        public bool DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id));
            }

            return _partnerRepository.DeleteAsync(id);
        }

        public List<Partner> GetRecommendListAsync(int topCount)
        {
            if (topCount == 0)
            {
                return null;
            }

            return _partnerRepository.GetRecommendListAsync(topCount);
        }
    }
}

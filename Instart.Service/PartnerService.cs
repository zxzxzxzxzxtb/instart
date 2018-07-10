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
            this.AddDisposableObject(_partnerRepository);
        }

        public async Task<List<Partner>> GetListAsync(int topCount)
        {
            if(topCount == 0)
            {
                return null;
            }

            return await _partnerRepository.GetListAsync(topCount);
        }
    }
}

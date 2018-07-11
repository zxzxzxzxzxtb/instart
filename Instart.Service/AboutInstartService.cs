using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instart.Models;
using Instart.Repository;

namespace Instart.Service
{
    public class AboutInstartService : ServiceBase, IAboutInstartService
    {
        IAboutInstartRepository _aboutInstartRepository = AutofacRepository.Resolve<IAboutInstartRepository>();

        public AboutInstartService()
        {
            base.AddDisposableObject(_aboutInstartRepository);
        }

        public async Task<int> GetCountAsync()
        {
            return await _aboutInstartRepository.GetCountAsync();
        }

        public async Task<AboutInstart> GetInfoAsync()
        {
            return await _aboutInstartRepository.GetInfoAsync();
        }

        public async Task<bool> InsertAsync(AboutInstart model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return await _aboutInstartRepository.InsertAsync(model);
        }

        public async Task<bool> UpdateAsync(AboutInstart model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return await _aboutInstartRepository.UpdateAsync(model);
        }
    }
}

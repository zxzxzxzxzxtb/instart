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

        public int GetCountAsync()
        {
            return _aboutInstartRepository.GetCountAsync();
        }

        public AboutInstart GetInfoAsync()
        {
            return _aboutInstartRepository.GetInfoAsync();
        }

        public bool InsertAsync(AboutInstart model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model不能为null");
            }

            return _aboutInstartRepository.InsertAsync(model);
        }

        public bool UpdateAsync(AboutInstart model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model不能为null");
            }

            return _aboutInstartRepository.UpdateAsync(model);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instart.Models;
using Instart.Repository;

namespace Instart.Service
{
    public class CopysService : ServiceBase, ICopysService
    {
        ICopysRepository _copysRepository = AutofacRepository.Resolve<ICopysRepository>();

        public CopysService()
        {
            base.AddDisposableObject(_copysRepository);
        }

        public int GetCountAsync()
        {
            return _copysRepository.GetCountAsync();
        }

        public Copys GetInfoAsync()
        {
            return _copysRepository.GetInfoAsync();
        }

        public bool InsertAsync(Copys model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model不能为null");
            }

            return _copysRepository.InsertAsync(model);
        }

        public bool UpdateAsync(Copys model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model不能为null");
            }

            return _copysRepository.UpdateAsync(model);
        }
    }
}

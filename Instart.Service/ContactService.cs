using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instart.Models;
using Instart.Repository;

namespace Instart.Service
{
    public class ContactService : ServiceBase, IContactService
    {
        IContactRepository _contactRepository = AutofacRepository.Resolve<IContactRepository>();

        public ContactService()
        {
            base.AddDisposableObject(_contactRepository);
        }

        public int GetCountAsync()
        {
            return _contactRepository.GetCountAsync();
        }

        public Contact GetInfoAsync()
        {
            return _contactRepository.GetInfoAsync();
        }

        public bool InsertAsync(Contact model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model不能为null");
            }

            return _contactRepository.InsertAsync(model);
        }

        public bool UpdateAsync(Contact model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model不能为null");
            }

            return _contactRepository.UpdateAsync(model);
        }
    }
}

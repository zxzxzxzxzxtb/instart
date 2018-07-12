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

        public async Task<int> GetCountAsync()
        {
            return await _contactRepository.GetCountAsync();
        }

        public async Task<Contact> GetInfoAsync()
        {
            return await _contactRepository.GetInfoAsync();
        }

        public async Task<bool> InsertAsync(Contact model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return await _contactRepository.InsertAsync(model);
        }

        public async Task<bool> UpdateAsync(Contact model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return await _contactRepository.UpdateAsync(model);
        }
    }
}

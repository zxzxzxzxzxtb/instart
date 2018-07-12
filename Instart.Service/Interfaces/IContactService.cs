using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instart.Models;

namespace Instart.Service
{
    public interface IContactService
    {
        Task<int> GetCountAsync();

        Task<Contact> GetInfoAsync();

        Task<bool> InsertAsync(Contact model);

        Task<bool> UpdateAsync(Contact model);
    }
}

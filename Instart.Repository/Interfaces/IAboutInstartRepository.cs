using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instart.Models;

namespace Instart.Repository
{
    public interface IAboutInstartRepository
    {
        Task<int> GetCountAsync();

        Task<AboutInstart> GetInfoAsync();

        Task<bool> InsertAsync(AboutInstart model);

        Task<bool> UpdateAsync(AboutInstart model);
    }
}

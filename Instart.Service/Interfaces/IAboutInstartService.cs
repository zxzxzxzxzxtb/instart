using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instart.Models;

namespace Instart.Service
{
    public interface IAboutInstartService
    {
        Task<int> GetCountAsync();

        Task<AboutInstart> GetInfoAsync();

        Task<bool> InsertAsync(AboutInstart model);

        Task<bool> UpdateAsync(AboutInstart model);
    }
}

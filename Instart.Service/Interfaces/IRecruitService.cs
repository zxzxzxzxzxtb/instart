using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instart.Models;

namespace Instart.Service
{
    public interface IRecruitService
    {
        Task<int> GetCountAsync();

        Task<Recruit> GetInfoAsync();

        Task<bool> InsertAsync(Recruit model);

        Task<bool> UpdateAsync(Recruit model);
    }
}

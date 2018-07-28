using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instart.Models;

namespace Instart.Repository
{
    public interface IRecruitRepository
    {
        Task<int> GetCountAsync();

        Task<Recruit> GetInfoAsync();

        Task<bool> InsertAsync(Recruit model);

        Task<bool> UpdateAsync(Recruit model);
    }
}

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
        int GetCountAsync();

        Recruit GetInfoAsync();

        bool InsertAsync(Recruit model);

        bool UpdateAsync(Recruit model);
    }
}

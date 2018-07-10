using Instart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Repository
{
    public interface IPartnerRepository
    {
        Task<List<Partner>> GetListAsync(int topCount);
    }
}

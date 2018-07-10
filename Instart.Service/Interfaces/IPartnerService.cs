using Instart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Service
{
    public interface IPartnerService
    {
        Task<List<Partner>> GetListAsync(int topCount);
    }
}

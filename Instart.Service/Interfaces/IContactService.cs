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
        int GetCountAsync();

        Contact GetInfoAsync();

        bool InsertAsync(Contact model);

        bool UpdateAsync(Contact model);
    }
}

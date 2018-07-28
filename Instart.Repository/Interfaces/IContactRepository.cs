using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instart.Models;

namespace Instart.Repository
{
    public interface IContactRepository
    {
        int GetCountAsync();

        Contact GetInfoAsync();

        bool InsertAsync(Contact model);

        bool UpdateAsync(Contact model);
    }
}

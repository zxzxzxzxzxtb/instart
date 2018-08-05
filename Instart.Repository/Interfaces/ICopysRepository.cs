using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instart.Models;

namespace Instart.Repository
{
    public interface ICopysRepository
    {
        int GetCountAsync();

        Copys GetInfoAsync();

        bool InsertAsync(Copys model);

        bool UpdateAsync(Copys model);
    }
}

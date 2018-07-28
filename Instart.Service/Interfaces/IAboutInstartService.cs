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
        int GetCountAsync();

        AboutInstart GetInfoAsync();

        bool InsertAsync(AboutInstart model);

        bool UpdateAsync(AboutInstart model);
    }
}

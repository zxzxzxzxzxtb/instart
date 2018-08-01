using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instart.Models;

namespace Instart.Service
{
    public interface IStudioService
    {
        int GetCountAsync();

        Studio GetInfoAsync();

        bool InsertAsync(Studio model);

        bool UpdateAsync(Studio model);

        IEnumerable<StudioImg> GetImgsAsync();

        bool InsertImgAsync(string imgUrl);

        bool DeleteImgAsync(int id);
    }
}

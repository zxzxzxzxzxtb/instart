using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instart.Models;

namespace Instart.Repository
{
    public interface IStudioRepository
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

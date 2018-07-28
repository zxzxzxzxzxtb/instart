using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instart.Models;

namespace Instart.Service
{
    public interface ICampusService
    {
        Campus GetByIdAsync(int id);

        PageModel<Campus> GetListAsync(int pageIndex, int pageSize, string name = null);

        IEnumerable<Campus> GetAllAsync();

        bool InsertAsync(Campus model);

        bool UpdateAsync(Campus model);

        bool DeleteAsync(int id);

        IEnumerable<CampusImg> GetImgsByCampusIdAsync(int campusId);

        bool InsertImgAsync(CampusImg model);

        bool DeleteImgAsync(int id);
    }
}

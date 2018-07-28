using Instart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Service
{
    public interface IStarStudentService
    {
         StarStudent GetByIdAsync(int id);

         PageModel<StarStudent> GetListAsync(int pageIndex, int pageSize, string name = null);

         IEnumerable<StarStudent> GetAllAsync();

         bool InsertAsync(StarStudent model);

         bool UpdateAsync(StarStudent model);

         bool DeleteAsync(int id);
    }
}

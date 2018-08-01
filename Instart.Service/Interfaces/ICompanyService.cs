using Instart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Service
{
    public interface ICompanyService
    {
         Company GetByIdAsync(int id);

         PageModel<Company> GetListAsync(int pageIndex, int pageSize, string name = null);

         IEnumerable<Company> GetAllAsync();

         bool InsertAsync(Company model);

         bool UpdateAsync(Company model);

         bool DeleteAsync(int id);
    }
}

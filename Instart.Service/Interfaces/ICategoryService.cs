using Instart.Models;
using Instart.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Service
{
    public interface ICategoryService
    {
        Category GetByIdAsync(int id);

        List<Category> GetByParentIdAsync(int parentId);

        PageModel<Category> GetListAsync(int pageIndex, int pageSize, string name = null);

        bool InsertAsync(Category model);

        bool UpdateAsync(Category model);

        bool DeleteAsync(int id);
    }
}

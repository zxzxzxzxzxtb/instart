using Instart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Repository
{
    public interface IArticleRepository
    {
        Article GetByIdAsync(int id);

        PageModel<Article> GetListAsync(int pageIndex, int pageSize, int categoryId = 0, string title = null);

        bool InsertAsync(Article model);

        bool UpdateAsync(Article model);

        bool DeleteAsync(int id);
    }
}

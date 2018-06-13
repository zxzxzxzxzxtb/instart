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
        Task<Article> GetByIdAsync(int id);

        Task<PageModel<Article>> GetListAsync(int pageIndex, int pageSize, int categoryId = 0, string title = null);

        Task<bool> InsertAsync(Article model);

        Task<bool> UpdateAsync(Article model);

        Task<bool> DeleteAsync(int id);
    }
}

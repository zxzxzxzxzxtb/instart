using Instart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Service
{
    public interface IArticleService
    {
        Task<Article> GetByIdAsync(int id);

        Task<PageModel<Article>> GetListAsync(int pageIndex, int pageSize, int categoryId = 0);

        Task<bool> InsertAsync(Article model);

        Task<bool> UpdateAsync(Article model);

        Task<bool> DeleteAsync(int id);
    }
}

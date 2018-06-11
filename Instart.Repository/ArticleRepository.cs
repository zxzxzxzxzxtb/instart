using Instart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Repository
{
    public class ArticleRepository : IArticleRepository
    {
        public async Task<Article> GetByIdAsync(int id)
        {
            return null;
        }

        public async Task<PageModel<Article>> GetListAsync(int pageIndex, int pageSize, int categoryId = 0)
        {
            var result = new PageModel<Article>();
            return result;
        }

        public async new Task<bool> AddAsync(Article model)
        {
            return false;
        }

        public async new Task<bool> UpdateAsync(Article model)
        {
            return false;
        }
    }
}

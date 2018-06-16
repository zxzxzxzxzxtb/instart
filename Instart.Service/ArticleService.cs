using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instart.Models;
using Instart.Repository;

namespace Instart.Service
{
    public class ArticleService : ServiceBase, IArticleService
    {
        IArticleRepository _articleRepository = AutofacRepository.Resolve<IArticleRepository>();

        public ArticleService()
        {
            base.AddDisposableObject(_articleRepository);
        }

        public async Task<Article> GetByIdAsync(int id)
        {
            if(id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            return await _articleRepository.GetByIdAsync(id);
        }

        public async Task<PageModel<Article>> GetListAsync(int pageIndex, int pageSize, int categoryId = 0, string title = null)
        {
            return await _articleRepository.GetListAsync(pageIndex, pageSize, categoryId, title);
        }

        public async Task<bool> InsertAsync(Article model)
        {
            if(model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrEmpty(model.Title)) {
                throw new ArgumentNullException(nameof(model.Title));
            }

            if (string.IsNullOrEmpty(model.Content)) {
                throw new ArgumentNullException(nameof(model.Content));
            }
            
            return await _articleRepository.InsertAsync(model);
        }

        public async Task<bool> UpdateAsync(Article model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrEmpty(model.Title)) {
                throw new ArgumentNullException(nameof(model.Title));
            }

            if (string.IsNullOrEmpty(model.Content)) {
                throw new ArgumentNullException(nameof(model.Content));
            }

            if(model.Id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(model.Id));
            }
            
            return await _articleRepository.UpdateAsync(model);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id));
            }

            return await _articleRepository.DeleteAsync(id);
        }
    }
}

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

        public async Task<PageModel<Article>> GetListAsync(int pageIndex, int pageSize, int categoryId = 0)
        {
            return await _articleRepository.GetListAsync(pageIndex, pageSize, categoryId);
        }

        public async Task<bool> AddAsync(Article model)
        {
            if(model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return await _articleRepository.AddAsync(model);
        }

        public async Task<bool> UpdateAsync(Article model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
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
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            var model = await _articleRepository.GetByIdAsync(id);
            if(model == null)
            {
                throw new Exception($"用户不存在, id:{id}");
            }

            model.IsDelete = true;
            return await _articleRepository.UpdateAsync(model);
        }
    }
}

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

        public Article GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException("id错误");
            }

            return _articleRepository.GetByIdAsync(id);
        }

        public PageModel<Article> GetListAsync(int pageIndex, int pageSize, int categoryId = 0, string title = null)
        {
            return _articleRepository.GetListAsync(pageIndex, pageSize, categoryId, title);
        }

        public bool InsertAsync(Article model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model不能为null");
            }

            if (string.IsNullOrEmpty(model.Title))
            {
                throw new ArgumentNullException("Title不能为null");
            }

            if (string.IsNullOrEmpty(model.Content))
            {
                throw new ArgumentNullException("Content不能为null");
            }

            return _articleRepository.InsertAsync(model);
        }

        public bool UpdateAsync(Article model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model不能为null");
            }

            if (string.IsNullOrEmpty(model.Title))
            {
                throw new ArgumentNullException("Title不能为null");
            }

            if (string.IsNullOrEmpty(model.Content))
            {
                throw new ArgumentNullException("Content不能为null");
            }

            if (model.Id <= 0)
            {
                throw new ArgumentOutOfRangeException("Id错误");
            }

            return _articleRepository.UpdateAsync(model);
        }

        public bool DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("id错误");
            }

            return _articleRepository.DeleteAsync(id);
        }
    }
}

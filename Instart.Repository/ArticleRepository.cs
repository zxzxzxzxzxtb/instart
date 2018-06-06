using Instart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Repository
{
    public class ArticleRepository : RepositoryBase<Article>, IArticleRepository
    {
        public async Task<Article> GetByIdAsync(int id)
        {
            var list = await base.GetAsync(p => p.Id == id && p.IsDelete == false, p => p);
            return list?.FirstOrDefault();
        }

        public async Task<PageModel<Article>> GetListAsync(int pageIndex, int pageSize, int categoryId = 0)
        {
            var result = new PageModel<Article>();

            Expression<Func<Article, bool>> filter = p => p.IsDelete == false;
            if (categoryId > 0)
            {
                filter = (p => p.CategoryId == categoryId);
            }

            Expression<Func<Article,Article>> selector = p => new Article
            {
                Id = p.Id,
                Title = p.Title,
                SubTitle = p.SubTitle,
                Author = p.Author,
                CategoryId = p.CategoryId,
                CategoryName = p.CategoryName,
                Summary = p.Summary,
                IsTop = p.IsTop,
                IsReommend = p.IsReommend,
                CreateTime = p.CreateTime,
                ModifyTime = p.ModifyTime,
                GroupIndex = p.GroupIndex,
                Source = p.Source
            };

            result.Total = await base.CountAsync(filter);
            result.Data = await base.GetAsync(filter, selector, pageIndex, pageSize, p => p.CreateTime, false);
            return result;
        }

        public async new Task<bool> AddAsync(Article model)
        {
            return await base.AddAsync(model);
        }

        public async new Task<bool> UpdateAsync(Article model)
        {
            return await base.UpdateAsync(model);
        }
    }
}

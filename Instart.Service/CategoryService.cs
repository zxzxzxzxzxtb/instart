using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instart.Models;
using Instart.Repository;

namespace Instart.Service
{
    public class CategoryService : ServiceBase, ICategoryService
    {
        ICategoryRepository _categoryRepository = AutofacRepository.Resolve<ICategoryRepository>();

        public CategoryService()
        {
            base.AddDisposableObject(_categoryRepository);
        }

        public Category GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("id错误");
            }

            return _categoryRepository.GetByIdAsync(id);
        }

        public List<Category> GetByParentIdAsync(int parentId)
        {
            if (parentId <= 0)
            {
                throw new ArgumentException("parentId错误");
            }

            return _categoryRepository.GetByParentIdAsync(parentId);
        }

        public PageModel<Category> GetListAsync(int pageIndex, int pageSize, string name = null)
        {
            return _categoryRepository.GetListAsync(pageIndex, pageSize, name);
        }

        public bool InsertAsync(Category model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model不能为null");
            }

            if (string.IsNullOrEmpty(model.Name))
            {
                throw new ArgumentNullException("Name不能为null");
            }

            return _categoryRepository.InsertAsync(model);
        }

        public bool UpdateAsync(Category model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model不能为null");
            }

            if (string.IsNullOrEmpty(model.Name))
            {
                throw new ArgumentNullException("Name不能为null");
            }

            if (model.Id <= 0)
            {
                throw new ArgumentException("Id错误");
            }

            return _categoryRepository.UpdateAsync(model);
        }

        public bool DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("id错误");
            }

            return _categoryRepository.DeleteAsync(id);
        }

    }
}

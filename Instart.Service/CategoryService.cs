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

        public CategoryService() {
            base.AddDisposableObject(_categoryRepository);
        }

        public async Task<Category> GetByIdAsync(int id) {
            if(id <= 0) {
                throw new ArgumentException(nameof(id));
            }

            return await _categoryRepository.GetByIdAsync(id);
        }

        public async Task<List<Category>> GetByParentIdAsync(int parentId) {
            if (parentId <= 0) {
                throw new ArgumentException(nameof(parentId));
            }

            return await _categoryRepository.GetByParentIdAsync(parentId);
        }

        public async Task<PageModel<Category>> GetListAsync(int pageIndex, int pageSize, string name = null) {
            return await _categoryRepository.GetListAsync(pageIndex, pageSize, name);
        }

        public async Task<bool> InsertAsync(Category model) {
            if (model == null) {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrEmpty(model.Name)) {
                throw new ArgumentNullException(nameof(model.Name));
            }
            
            return await _categoryRepository.InsertAsync(model);
        }

        public async Task<bool> UpdateAsync(Category model) {
            if (model == null) {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrEmpty(model.Name)) {
                throw new ArgumentNullException(nameof(model.Name));
            }

            if (model.Id <= 0) {
                throw new ArgumentException(nameof(model.Id));
            }
            
            return await _categoryRepository.UpdateAsync(model);
        }

        public async Task<bool> DeleteAsync(int id) {
            if (id <= 0) {
                throw new ArgumentException(nameof(id));
            }

            return await _categoryRepository.DeleteAsync(id);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instart.Models;
using Instart.Repository;

namespace Instart.Service
{
    public class MajorService : ServiceBase, IMajorService
    {
        IMajorRepository _majorRepository = AutofacRepository.Resolve<IMajorRepository>();

        public MajorService() {
            base.AddDisposableObject(_majorRepository);
        }

        public async Task<Major> GetByIdAsync(int id) {
            if (id <= 0) {
                throw new ArgumentException(nameof(id));
            }

            return await _majorRepository.GetByIdAsync(id);
        }

        public async Task<PageModel<Major>> GetListAsync(int pageIndex, int pageSize, string name = null) {
            return await _majorRepository.GetListAsync(pageIndex, pageSize, name);
        }

        public async Task<IEnumerable<Major>> GetAllAsync()
        {
            return await _majorRepository.GetAllAsync();
        }

        public async Task<bool> InsertAsync(Major model) {
            if (model == null) {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrEmpty(model.Name)) {
                throw new ArgumentNullException(nameof(model.Name));
            }

            return await _majorRepository.InsertAsync(model);
        }

        public async Task<bool> UpdateAsync(Major model) {
            if (model == null) {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrEmpty(model.Name)) {
                throw new ArgumentNullException(nameof(model.Name));
            }

            if (model.Id <= 0) {
                throw new ArgumentException(nameof(model.Id));
            }

            return await _majorRepository.UpdateAsync(model);
        }

        public async Task<bool> DeleteAsync(int id) {
            if (id <= 0) {
                throw new ArgumentException(nameof(id));
            }

            return await _majorRepository.DeleteAsync(id);
        }
    }
}

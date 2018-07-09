using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instart.Models;
using Instart.Repository;

namespace Instart.Service
{
    public class WorksService : ServiceBase, IWorksService
    {
        IWorksRepository _worksRepository = AutofacRepository.Resolve<IWorksRepository>();

        public WorksService() {
            base.AddDisposableObject(_worksRepository);
        }

        public async Task<Works> GetByIdAsync(int id) {
            if (id <= 0) {
                throw new ArgumentException(nameof(id));
            }

            return await _worksRepository.GetByIdAsync(id);
        }

        public async Task<PageModel<Works>> GetListAsync(int pageIndex, int pageSize, string name = null) {
            return await _worksRepository.GetListAsync(pageIndex, pageSize, name);
        }

        public async Task<bool> InsertAsync(Works model) {
            if (model == null) {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrEmpty(model.ImgName)) {
                throw new ArgumentNullException(nameof(model.ImgName));
            }

            return await _worksRepository.InsertAsync(model);
        }

        public async Task<bool> UpdateAsync(Works model) {
            if (model == null) {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrEmpty(model.ImgName)) {
                throw new ArgumentNullException(nameof(model.ImgName));
            }

            if (model.Id <= 0) {
                throw new ArgumentException(nameof(model.Id));
            }

            return await _worksRepository.UpdateAsync(model);
        }

        public async Task<bool> DeleteAsync(int id) {
            if (id <= 0) {
                throw new ArgumentException(nameof(id));
            }

            return await _worksRepository.DeleteAsync(id);
        }
    }
}

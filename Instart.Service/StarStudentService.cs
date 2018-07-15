using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instart.Models;
using Instart.Repository;

namespace Instart.Service
{
    public class StarStudentService : ServiceBase, IStarStudentService
    {
        IStarStudentRepository _starStudentRepository = AutofacRepository.Resolve<IStarStudentRepository>();

        public StarStudentService() {
            base.AddDisposableObject(_starStudentRepository);
        }

        public async Task<StarStudent> GetByIdAsync(int id) {
            if (id <= 0) {
                throw new ArgumentException(nameof(id));
            }

            return await _starStudentRepository.GetByIdAsync(id);
        }

        public async Task<PageModel<StarStudent>> GetListAsync(int pageIndex, int pageSize, string name = null) {
            return await _starStudentRepository.GetListAsync(pageIndex, pageSize, name);
        }

        public async Task<IEnumerable<StarStudent>> GetAllAsync()
        {
            return await _starStudentRepository.GetAllAsync();
        }

        public async Task<bool> InsertAsync(StarStudent model) {
            if (model == null) {
                throw new ArgumentNullException(nameof(model));
            }

            return await _starStudentRepository.InsertAsync(model);
        }

        public async Task<bool> UpdateAsync(StarStudent model) {
            if (model == null) {
                throw new ArgumentNullException(nameof(model));
            }

            if (model.Id <= 0) {
                throw new ArgumentException(nameof(model.Id));
            }

            return await _starStudentRepository.UpdateAsync(model);
        }

        public async Task<bool> DeleteAsync(int id) {
            if (id <= 0) {
                throw new ArgumentException(nameof(id));
            }

            return await _starStudentRepository.DeleteAsync(id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instart.Models;
using Instart.Repository;

namespace Instart.Service
{
    public class CampusService : ServiceBase, ICampusService
    {
        ICampusRepository _campusRepository = AutofacRepository.Resolve<ICampusRepository>();

        public CampusService()
        {
            base.AddDisposableObject(_campusRepository);
        }

        public async Task<Campus> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id));
            }
            return await _campusRepository.GetByIdAsync(id);
        }

        public async Task<PageModel<Campus>> GetListAsync(int pageIndex, int pageSize, string name = null)
        {
            return await _campusRepository.GetListAsync(pageIndex, pageSize, name);
        }

        public async Task<IEnumerable<Campus>> GetAllAsync()
        {
            return await _campusRepository.GetAllAsync();
        }

        public async Task<bool> InsertAsync(Campus model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrEmpty(model.Name))
            {
                throw new ArgumentNullException(nameof(model.Name));
            }

            return await _campusRepository.InsertAsync(model);
        }

        public async Task<bool> UpdateAsync(Campus model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrEmpty(model.Name))
            {
                throw new ArgumentNullException(nameof(model.Name));
            }

            if (model.Id <= 0)
            {
                throw new ArgumentException(nameof(model.Id));
            }

            return await _campusRepository.UpdateAsync(model);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id));
            }

            return await _campusRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<CampusImg>> GetImgsByCampusIdAsync(int campusId)
        {
            return await _campusRepository.GetImgsByCampusIdAsync(campusId);
        }

        public async Task<bool> InsertImgAsync(CampusImg model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            return await _campusRepository.InsertImgAsync(model);
        }

        public async Task<bool> DeleteImgAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id));
            }

            return await _campusRepository.DeleteImgAsync(id);
        }
    }
}

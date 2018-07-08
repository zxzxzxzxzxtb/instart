using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instart.Models;
using Instart.Repository;

namespace Instart.Service
{
    public class DivisionService : ServiceBase, IDivisionService
    {
        IDivisionRepository _divisionRepository = AutofacRepository.Resolve<IDivisionRepository>();

        public DivisionService()
        {
            base.AddDisposableObject(_divisionRepository);
        }

        public async Task<Division> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id));
            }

            return await _divisionRepository.GetByIdAsync(id);
        }

        public async Task<PageModel<Division>> GetListAsync(int pageIndex, int pageSize, string name = null)
        {
            return await _divisionRepository.GetListAsync(pageIndex, pageSize, name);
        }

        public async Task<IEnumerable<Division>> GetAllAsync()
        {
            return await _divisionRepository.GetAllAsync();
        }

        public async Task<bool> InsertAsync(Division model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrEmpty(model.Name))
            {
                throw new ArgumentNullException(nameof(model.Name));
            }

            return await _divisionRepository.InsertAsync(model);
        }

        public async Task<bool> UpdateAsync(Division model)
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

            return await _divisionRepository.UpdateAsync(model);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id));
            }

            return await _divisionRepository.DeleteAsync(id);
        }
    }
}

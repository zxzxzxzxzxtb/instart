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

        public Division GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id));
            }

            return _divisionRepository.GetByIdAsync(id);
        }

        public PageModel<Division> GetListAsync(int pageIndex, int pageSize, string name = null)
        {
            return _divisionRepository.GetListAsync(pageIndex, pageSize, name);
        }

        public IEnumerable<Division> GetAllAsync()
        {
            return _divisionRepository.GetAllAsync();
        }

        public bool InsertAsync(Division model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrEmpty(model.Name))
            {
                throw new ArgumentNullException(nameof(model.Name));
            }

            return _divisionRepository.InsertAsync(model);
        }

        public bool UpdateAsync(Division model)
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

            return _divisionRepository.UpdateAsync(model);
        }

        public bool DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id));
            }

            return _divisionRepository.DeleteAsync(id);
        }
    }
}

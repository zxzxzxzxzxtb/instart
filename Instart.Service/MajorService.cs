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

        public MajorService()
        {
            base.AddDisposableObject(_majorRepository);
        }

        public Major GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id));
            }

            return _majorRepository.GetByIdAsync(id);
        }

        public PageModel<Major> GetListAsync(int pageIndex, int pageSize, int division = -1, string name = null)
        {
            return _majorRepository.GetListAsync(pageIndex, pageSize, division, name);
        }

        public IEnumerable<Major> GetAllAsync()
        {
            return _majorRepository.GetAllAsync();
        }

        public bool InsertAsync(Major model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrEmpty(model.Name))
            {
                throw new ArgumentNullException(nameof(model.Name));
            }

            return _majorRepository.InsertAsync(model);
        }

        public bool UpdateAsync(Major model)
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

            return _majorRepository.UpdateAsync(model);
        }

        public bool DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id));
            }

            return _majorRepository.DeleteAsync(id);
        }

        public PageModel<Major> GetListByDivsionAsync(int divisionId, int pageIndex, int pageSize)
        {
            if (divisionId <= 0)
            {
                throw new ArgumentException(nameof(Major));
            }

            return _majorRepository.GetListByDivsionAsync(divisionId, pageIndex, pageSize);
        }
    }
}

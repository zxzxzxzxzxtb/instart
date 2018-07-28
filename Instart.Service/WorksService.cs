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

        public WorksService()
        {
            base.AddDisposableObject(_worksRepository);
        }

        public Works GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id));
            }

            return _worksRepository.GetByIdAsync(id);
        }

        public PageModel<Works> GetListAsync(int pageIndex, int pageSize, string name = null)
        {
            return _worksRepository.GetListAsync(pageIndex, pageSize, name);
        }

        public bool InsertAsync(Works model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrEmpty(model.Name))
            {
                throw new ArgumentNullException(nameof(model.Name));
            }

            return _worksRepository.InsertAsync(model);
        }

        public bool UpdateAsync(Works model)
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

            return _worksRepository.UpdateAsync(model);
        }

        public bool DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id));
            }

            return _worksRepository.DeleteAsync(id);
        }

        public List<Works> GetListByMajorIdAsync(int majorId, int topCount)
        {
            if (majorId <= 0)
            {
                throw new ArgumentException(nameof(majorId));
            }

            return _worksRepository.GetListByMajorIdAsync(majorId, topCount);
        }
    }
}

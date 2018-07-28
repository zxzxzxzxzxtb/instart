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

        public StarStudentService()
        {
            base.AddDisposableObject(_starStudentRepository);
        }

        public StarStudent GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id));
            }

            return _starStudentRepository.GetByIdAsync(id);
        }

        public PageModel<StarStudent> GetListAsync(int pageIndex, int pageSize, string name = null)
        {
            return _starStudentRepository.GetListAsync(pageIndex, pageSize, name);
        }

        public IEnumerable<StarStudent> GetAllAsync()
        {
            return _starStudentRepository.GetAllAsync();
        }

        public bool InsertAsync(StarStudent model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return _starStudentRepository.InsertAsync(model);
        }

        public bool UpdateAsync(StarStudent model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (model.Id <= 0)
            {
                throw new ArgumentException(nameof(model.Id));
            }

            return _starStudentRepository.UpdateAsync(model);
        }

        public bool DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id));
            }

            return _starStudentRepository.DeleteAsync(id);
        }
    }
}

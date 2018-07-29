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
                throw new ArgumentException("id错误");
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
                throw new ArgumentNullException("model不能为null");
            }

            return _starStudentRepository.InsertAsync(model);
        }

        public bool UpdateAsync(StarStudent model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model不能为null");
            }

            if (model.Id <= 0)
            {
                throw new ArgumentException("Id错误");
            }

            return _starStudentRepository.UpdateAsync(model);
        }

        public bool DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("id错误");
            }

            return _starStudentRepository.DeleteAsync(id);
        }
    }
}

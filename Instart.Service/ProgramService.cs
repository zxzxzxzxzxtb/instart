using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instart.Models;
using Instart.Repository;

namespace Instart.Service
{
    public class ProgramService : ServiceBase, IProgramService
    {
        IProgramRepository _programRepository = AutofacRepository.Resolve<IProgramRepository>();

        public ProgramService()
        {
            base.AddDisposableObject(_programRepository);
        }

        public Program GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("id不能为空");
            }

            return _programRepository.GetByIdAsync(id);
        }

        public PageModel<Program> GetListAsync(int pageIndex, int pageSize, int type = -1, string name = null)
        {
            return _programRepository.GetListAsync(pageIndex, pageSize, type, name);
        }

        public IEnumerable<Program> GetAllAsync()
        {
            return _programRepository.GetAllAsync();
        }

        public bool InsertAsync(Program model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model不能为null");
            }

            if (string.IsNullOrEmpty(model.Name))
            {
                throw new ArgumentNullException("项目名称不能为null");
            }

            return _programRepository.InsertAsync(model);
        }

        public bool UpdateAsync(Program model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model不能为null");
            }

            if (string.IsNullOrEmpty(model.Name))
            {
                throw new ArgumentNullException("项目名称不能为null");
            }

            if (model.Id <= 0)
            {
                throw new ArgumentException("Id不能为null");
            }

            return _programRepository.UpdateAsync(model);
        }

        public bool DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Id不能为null");
            }

            return _programRepository.DeleteAsync(id);
        }

        public IEnumerable<Program> GetListByTypeAsync(int type)
        {
            return _programRepository.GetListByTypeAsync(type);
        }
    }
}

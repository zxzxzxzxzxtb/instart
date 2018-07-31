using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instart.Models;
using Instart.Repository;

namespace Instart.Service
{
    public class CompanyService : ServiceBase, ICompanyService
    {
        ICompanyRepository _companyRepository = AutofacRepository.Resolve<ICompanyRepository>();

        public CompanyService()
        {
            base.AddDisposableObject(_companyRepository);
        }

        public Company GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("id不能为空");
            }

            return _companyRepository.GetByIdAsync(id);
        }

        public PageModel<Company> GetListAsync(int pageIndex, int pageSize, string name = null)
        {
            return _companyRepository.GetListAsync(pageIndex, pageSize, name);
        }

        public IEnumerable<Company> GetAllAsync()
        {
            return _companyRepository.GetAllAsync();
        }

        public bool InsertAsync(Company model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model不能为null");
            }

            if (string.IsNullOrEmpty(model.Name))
            {
                throw new ArgumentNullException("Name不能为null");
            }

            return _companyRepository.InsertAsync(model);
        }

        public bool UpdateAsync(Company model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model不能为null");
            }

            if (string.IsNullOrEmpty(model.Name))
            {
                throw new ArgumentNullException("Name不能为null");
            }

            if (model.Id <= 0)
            {
                throw new ArgumentException("Id错误");
            }

            return _companyRepository.UpdateAsync(model);
        }

        public bool DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("id错误");
            }

            return _companyRepository.DeleteAsync(id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instart.Models;
using Instart.Models.Enums;
using Instart.Repository;

namespace Instart.Service
{
    public class CompanyApplyService : ServiceBase, ICompanyApplyService
    {
        ICompanyApplyRepository _companyApplyRepository = AutofacRepository.Resolve<ICompanyApplyRepository>();

        public CompanyApplyService()
        {
            base.AddDisposableObject(_companyApplyRepository);
        }

        public PageModel<CompanyApply> GetListAsync(int pageIndex, int pageSize, string name, EnumAccept accept)
        {
            return _companyApplyRepository.GetListAsync(pageIndex, pageSize, name, accept);
        }

        public bool InsertAsync(CompanyApply model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model不能为null");
            }

            if (string.IsNullOrEmpty(model.Name))
            {
                throw new ArgumentNullException("姓名不能为空");
            }

            if (string.IsNullOrEmpty(model.Phone))
            {
                throw new ArgumentNullException("手机不能为空");
            }

            return _companyApplyRepository.InsertAsync(model);
        }

        public bool SetAcceptAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException("id错误");
            }

            return _companyApplyRepository.SetAcceptAsync(id);
        }

        public List<CompanyApply> GetTopListAsync(int topCount)
        {
            if (topCount == 0)
            {
                return null;
            }

            return _companyApplyRepository.GetTopListAsync(topCount);
        }
    }
}

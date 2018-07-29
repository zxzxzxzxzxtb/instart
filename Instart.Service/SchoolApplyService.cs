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
    public class SchoolApplyService : ServiceBase, ISchoolApplyService
    {
        ISchoolApplyRepository _schoolApplyRepository = AutofacRepository.Resolve<ISchoolApplyRepository>();

        public SchoolApplyService()
        {
            base.AddDisposableObject(_schoolApplyRepository);
        }

        public List<string> GetApplySchoolNameListAsync()
        {
            return _schoolApplyRepository.GetApplySchoolNameListAsync();
        }

        public PageModel<SchoolApply> GetListAsync(int pageIndex, int pageSize, string schoolName, EnumAccept accept)
        {
            return _schoolApplyRepository.GetListAsync(pageIndex, pageSize, schoolName, accept);
        }

        public bool InsertAsync(SchoolApply model)
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

            return _schoolApplyRepository.InsertAsync(model);
        }

        public bool SetAcceptAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException("id错误");
            }

            return _schoolApplyRepository.SetAcceptAsync(id);
        }

        public List<SchoolApply> GetTopListAsync(int topCount)
        {
            if (topCount == 0)
            {
                return null;
            }

            return _schoolApplyRepository.GetTopListAsync(topCount);
        }
    }
}

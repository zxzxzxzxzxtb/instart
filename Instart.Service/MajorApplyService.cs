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
    public class MajorApplyService : ServiceBase, IMajorApplyService
    {
        IMajorApplyRepository _majorApplyRepository = AutofacRepository.Resolve<IMajorApplyRepository>();

        public MajorApplyService()
        {
            base.AddDisposableObject(_majorApplyRepository);
        }

        public PageModel<MajorApply> GetListAsync(int pageIndex, int pageSize, string name, EnumAccept accept)
        {
            return _majorApplyRepository.GetListAsync(pageIndex, pageSize, name, accept);
        }

        public bool InsertAsync(MajorApply model)
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

            return _majorApplyRepository.InsertAsync(model);
        }

        public bool SetAcceptAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException("id错误");
            }

            return _majorApplyRepository.SetAcceptAsync(id);
        }

        public List<MajorApply> GetTopListAsync(int topCount)
        {
            if (topCount == 0)
            {
                return null;
            }

            return _majorApplyRepository.GetTopListAsync(topCount);
        }
    }
}

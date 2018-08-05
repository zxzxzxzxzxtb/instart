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
    public class HereMoreService : ServiceBase, IHereMoreService
    {
        IHereMoreRepository _hereMoreRepository = AutofacRepository.Resolve<IHereMoreRepository>();

        public HereMoreService()
        {
            base.AddDisposableObject(_hereMoreRepository);
        }

        public PageModel<HereMore> GetListAsync(int pageIndex, int pageSize, string name, EnumAccept accept)
        {
            return _hereMoreRepository.GetListAsync(pageIndex, pageSize, name, accept);
        }

        public bool InsertAsync(HereMore model)
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

            return _hereMoreRepository.InsertAsync(model);
        }

        public bool SetAcceptAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException("id错误");
            }

            return _hereMoreRepository.SetAcceptAsync(id);
        }

        public List<HereMore> GetTopListAsync(int topCount)
        {
            if (topCount == 0)
            {
                return null;
            }

            return _hereMoreRepository.GetTopListAsync(topCount);
        }
    }
}

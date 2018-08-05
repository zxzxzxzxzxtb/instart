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
    public class WorksCommentService : ServiceBase, IWorksCommentService
    {
        IWorksCommentRepository _worksCommentRepository = AutofacRepository.Resolve<IWorksCommentRepository>();

        public WorksCommentService()
        {
            base.AddDisposableObject(_worksCommentRepository);
        }

        public PageModel<WorksComment> GetListAsync(int pageIndex, int pageSize, string name, EnumAccept accept)
        {
            return _worksCommentRepository.GetListAsync(pageIndex, pageSize, name, accept);
        }

        public bool InsertAsync(WorksComment model)
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

            return _worksCommentRepository.InsertAsync(model);
        }

        public bool SetAcceptAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException("id错误");
            }

            return _worksCommentRepository.SetAcceptAsync(id);
        }

        public List<WorksComment> GetTopListAsync(int topCount)
        {
            if (topCount == 0)
            {
                return null;
            }

            return _worksCommentRepository.GetTopListAsync(topCount);
        }
    }
}

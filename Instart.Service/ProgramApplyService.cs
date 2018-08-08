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
    public class ProgramApplyService : ServiceBase, IProgramApplyService
    {
        IProgramApplyRepository _programApplyRepository = AutofacRepository.Resolve<IProgramApplyRepository>();

        public ProgramApplyService()
        {
            base.AddDisposableObject(_programApplyRepository);
        }

        public PageModel<ProgramApply> GetListAsync(int pageIndex, int pageSize, string programName, EnumAccept accept)
        {
            return _programApplyRepository.GetListAsync(pageIndex, pageSize, programName, accept);
        }

        public List<ProgramApply> GetTopListAsync(int topCount)
        {
            if(topCount == 0)
            {
                return null;
            }

            return _programApplyRepository.GetTopListAsync(topCount);
        }

        public bool InsertAsync(ProgramApply model)
        {
            if(model == null)
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

            return _programApplyRepository.InsertAsync(model);
        }

        public  bool SetAcceptAsync(int id)
        {
            if(id <= 0)
            {
                throw new ArgumentNullException("id错误");
            }

            return _programApplyRepository.SetAcceptAsync(id);
        }
    }
}

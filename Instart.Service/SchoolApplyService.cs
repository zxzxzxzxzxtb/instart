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

        public async Task<List<string>> GetApplySchoolNameListAsync()
        {
            return await _schoolApplyRepository.GetApplySchoolNameListAsync();
        }

        public async Task<PageModel<SchoolApply>> GetListAsync(int pageIndex, int pageSize, string schoolName, EnumAccept accept)
        {
            return await _schoolApplyRepository.GetListAsync(pageIndex, pageSize, schoolName, accept);
        }

        public async Task<bool> InsertAsync(SchoolApply model)
        {
            if(model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrEmpty(model.Name))
            {
                throw new ArgumentNullException("姓名不能为空");
            }

            if (string.IsNullOrEmpty(model.Phone))
            {
                throw new ArgumentNullException("手机不能为空");
            }

            return await _schoolApplyRepository.InsertAsync(model);
        }

        public async Task<bool> SetAcceptAsync(int id)
        {
            if(id <= 0)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return await _schoolApplyRepository.SetAcceptAsync(id);
        }

        public async Task<List<SchoolApply>> GetTopListAsync(int topCount)
        {
            if(topCount == 0)
            {
                return null;
            }

            return await _schoolApplyRepository.GetTopListAsync(topCount);
        }
    }
}

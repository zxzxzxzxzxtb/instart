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
    public class CourseApplyService : ServiceBase, ICourseApplyService
    {
        ICourseApplyRepository _courseApplyRepository = AutofacRepository.Resolve<ICourseApplyRepository>();

        public CourseApplyService()
        {
            base.AddDisposableObject(_courseApplyRepository);
        }

        public async Task<List<string>> GetApplyCourseNameListAsync()
        {
            return await _courseApplyRepository.GetApplyCourseNameListAsync();
        }

        public async Task<PageModel<CourseApply>> GetListAsync(int pageIndex, int pageSize, string courseName, EnumAccept accept)
        {
            return await _courseApplyRepository.GetListAsync(pageIndex, pageSize, courseName, accept);
        }

        public async Task<List<CourseApply>> GetTopListAsync(int topCount)
        {
            if(topCount == 0)
            {
                return null;
            }

            return await _courseApplyRepository.GetTopListAsync(topCount);
        }

        public async Task<bool> InsertAsync(CourseApply model)
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

            return await _courseApplyRepository.InsertAsync(model);
        }

        public async Task<bool> SetAcceptAsync(int id)
        {
            if(id <= 0)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return await _courseApplyRepository.SetAcceptAsync(id);
        }
    }
}

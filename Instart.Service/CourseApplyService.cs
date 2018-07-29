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

        public  List<string> GetApplyCourseNameListAsync()
        {
            return _courseApplyRepository.GetApplyCourseNameListAsync();
        }

        public  PageModel<CourseApply> GetListAsync(int pageIndex, int pageSize, string courseName, EnumAccept accept)
        {
            return _courseApplyRepository.GetListAsync(pageIndex, pageSize, courseName, accept);
        }

        public  List<CourseApply> GetTopListAsync(int topCount)
        {
            if(topCount == 0)
            {
                return null;
            }

            return _courseApplyRepository.GetTopListAsync(topCount);
        }

        public  bool InsertAsync(CourseApply model)
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

            return _courseApplyRepository.InsertAsync(model);
        }

        public  bool SetAcceptAsync(int id)
        {
            if(id <= 0)
            {
                throw new ArgumentNullException("id错误");
            }

            return _courseApplyRepository.SetAcceptAsync(id);
        }
    }
}

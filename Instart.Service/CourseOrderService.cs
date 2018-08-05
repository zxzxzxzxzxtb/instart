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
    public class CourseOrderService : ServiceBase, ICourseOrderService
    {
        ICourseOrderRepository _courseOrderRepository = AutofacRepository.Resolve<ICourseOrderRepository>();

        public CourseOrderService()
        {
            base.AddDisposableObject(_courseOrderRepository);
        }

        public  PageModel<CourseOrder> GetListAsync(int pageIndex, int pageSize, string courseName, EnumAccept accept)
        {
            return _courseOrderRepository.GetListAsync(pageIndex, pageSize, courseName, accept);
        }

        public  List<CourseOrder> GetTopListAsync(int topCount)
        {
            if(topCount == 0)
            {
                return null;
            }

            return _courseOrderRepository.GetTopListAsync(topCount);
        }

        public  bool InsertAsync(CourseOrder model)
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

            return _courseOrderRepository.InsertAsync(model);
        }

        public  bool SetAcceptAsync(int id)
        {
            if(id <= 0)
            {
                throw new ArgumentNullException("id错误");
            }

            return _courseOrderRepository.SetAcceptAsync(id);
        }
    }
}

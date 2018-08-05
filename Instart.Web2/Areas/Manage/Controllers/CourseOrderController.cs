using Instart.Common;
using Instart.Models.Enums;
using Instart.Service;
using Instart.Service.Base;
using Instart.Web2.Attributes;
using Instart.Web2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Instart.Web2.Areas.Manage.Controllers
{
    [AdminValidation]
    public class CourseOrderController : ManageControllerBase
    {
        ICourseOrderService _courseOrderService = AutofacService.Resolve<ICourseOrderService>();

        public CourseOrderController()
        {
            base.AddDisposableObject(_courseOrderService);
        }

        public ActionResult Index(int page = 1, string keyword = null, EnumAccept accept = EnumAccept.All)
        {
            int pageSize = 10;
            var list = _courseOrderService.GetListAsync(page, pageSize, keyword, accept);
            ViewBag.Total = list.Total;
            ViewBag.PageIndex = page;
            ViewBag.TotalPages = Math.Ceiling(list.Total * 1.0 / pageSize);
            ViewBag.keyword = keyword;
            ViewBag.Accept = accept;
            return View(list.Data);
        }

        [HttpPost]
        [Operation("受理课程预约")]
        public JsonResult SetAccept(int id)
        {
            if (id <= 0)
            {
                return Error("id错误");
            }

            try
            {
                return Json(new ResultBase
                {
                    success = _courseOrderService.SetAcceptAsync(id)
                });
            }
            catch (Exception ex)
            {
                LogHelper.Error("CourseOrderController.SetAccept异常", ex);
                return Error(ex.Message);
            }
        }
    }
}
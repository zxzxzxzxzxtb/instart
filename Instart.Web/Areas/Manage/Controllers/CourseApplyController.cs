using Instart.Common;
using Instart.Models.Enums;
using Instart.Service;
using Instart.Service.Base;
using Instart.Web.Attributes;
using Instart.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Instart.Web.Areas.Manage.Controllers
{
    [AdminValidation]
    public class CourseApplyController : ManageControllerBase
    {
        ICourseApplyService _courseApplyService = AutofacService.Resolve<ICourseApplyService>();

        public CourseApplyController()
        {
            base.AddDisposableObject(_courseApplyService);
        }

        public ActionResult Index(int page = 1, string keyword = null, EnumAccept accept = EnumAccept.All)
        {
            int pageSize = 10;
            var list = _courseApplyService.GetListAsync(page, pageSize, keyword, accept);
            ViewBag.Total = list.Total;
            ViewBag.PageIndex = page;
            ViewBag.TotalPages = Math.Ceiling(list.Total * 1.0 / pageSize);
            ViewBag.keyword = keyword;
            ViewBag.Accept = accept;
            return View(list.Data);
        }

        [HttpPost]
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
                    success = _courseApplyService.SetAcceptAsync(id)
                });
            }
            catch (Exception ex)
            {
                LogHelper.Error($"CourseApplyController.SetAccept异常", ex);
                return Error(ex.Message);
            }
        }
    }
}
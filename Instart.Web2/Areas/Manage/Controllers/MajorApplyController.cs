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
    public class MajorApplyController : ManageControllerBase
    {
        IMajorApplyService _majorApplyService = AutofacService.Resolve<IMajorApplyService>();

        public MajorApplyController()
        {
            base.AddDisposableObject(_majorApplyService);
        }

        public ActionResult Index(int page = 1, string keyword = null, EnumAccept accept = EnumAccept.All)
        {
            int pageSize = 10;
            var list = _majorApplyService.GetListAsync(page, pageSize, keyword, accept);
            ViewBag.Total = list.Total;
            ViewBag.PageIndex = page;
            ViewBag.TotalPages = Math.Ceiling(list.Total * 1.0 / pageSize);
            ViewBag.keyword = keyword;
            ViewBag.Accept = accept;
            return View(list.Data);
        }

        [HttpPost]
        [Operation("专业咨询")]
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
                    success = _majorApplyService.SetAcceptAsync(id)
                });
            }
            catch (Exception ex)
            {
                LogHelper.Error("MajorApplyController.SetAccept异常", ex);
                return Error(ex.Message);
            }
        }
    }
}
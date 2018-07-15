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
    public class SchoolApplyController : ManageControllerBase
    {
        ISchoolApplyService _schoolApplyService = AutofacService.Resolve<ISchoolApplyService>();

        public SchoolApplyController()
        {
            base.AddDisposableObject(_schoolApplyService);
        }

        public async Task<ActionResult> Index(int page = 1, string keyword = null, EnumAccept accept = EnumAccept.All)
        {
            int pageSize = 10;
            var list = await _schoolApplyService.GetListAsync(page, pageSize, keyword, accept);
            ViewBag.Total = list.Total;
            ViewBag.PageIndex = page;
            ViewBag.TotalPages = Math.Ceiling(list.Total * 1.0 / pageSize);
            ViewBag.keyword = keyword;
            ViewBag.Accept = accept;
            return View(list.Data);
        }

        [HttpPost]
        public async Task<JsonResult> SetAccept(int id)
        {
            if (id <= 0)
            {
                return Error("id错误");
            }

            try
            {
                return Json(new ResultBase
                {
                    success = await _schoolApplyService.SetAcceptAsync(id)
                });
            }
            catch (Exception ex)
            {
                LogHelper.Error($"SchoolApplyController.SetAccept异常", ex);
                return Error(ex.Message);
            }
        }
    }
}
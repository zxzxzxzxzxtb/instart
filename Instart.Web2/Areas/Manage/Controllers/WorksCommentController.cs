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
    public class WorksCommentController : ManageControllerBase
    {
        IWorksCommentService _worksCommentService = AutofacService.Resolve<IWorksCommentService>();

        public WorksCommentController()
        {
            base.AddDisposableObject(_worksCommentService);
        }

        public ActionResult Index(int page = 1, string keyword = null, EnumAccept accept = EnumAccept.All)
        {
            int pageSize = 10;
            var list = _worksCommentService.GetListAsync(page, pageSize, keyword, accept);
            ViewBag.Total = list.Total;
            ViewBag.PageIndex = page;
            ViewBag.TotalPages = Math.Ceiling(list.Total * 1.0 / pageSize);
            ViewBag.keyword = keyword;
            ViewBag.Accept = accept;
            return View(list.Data);
        }

        [HttpPost]
        [Operation("受理作品评析")]
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
                    success = _worksCommentService.SetAcceptAsync(id)
                });
            }
            catch (Exception ex)
            {
                LogHelper.Error("WorksCommentController.SetAccept异常", ex);
                return Error(ex.Message);
            }
        }
    }
}
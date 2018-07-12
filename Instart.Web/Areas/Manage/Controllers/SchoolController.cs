using Instart.Common;
using Instart.Models;
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
    public class SchoolController : ManageControllerBase
    {
        ISchoolService _schoolService = AutofacService.Resolve<ISchoolService>();

        public SchoolController()
        {
            base.AddDisposableObject(_schoolService);
        }

        public async Task<ActionResult> Index(int page = 1, string keyword = null)
        {
            int pageSize = 10;
            var list = await _schoolService.GetListAsync(page, pageSize, keyword);
            ViewBag.Total = list.Total;
            ViewBag.PageIndex = page;
            ViewBag.TotalPages = Math.Ceiling(list.Total * 1.0 / pageSize);
            ViewBag.Keyword = keyword;
            return View(list.Data);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id = 0)
        {
            School model = new School();
            string action = "添加学校"; 

            if(id > 0)
            {
                model = await _schoolService.GetByIdAsync(id);
                action = "修改学校";
            }

            ViewBag.Action = action;
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            if(id <= 0)
            {
                return Error("id错误");
            }

            try
            {
                return Json(new ResultBase
                {
                    success = await _schoolService.DeleteAsync(id)
                });
            }
            catch (Exception ex)
            {
                LogHelper.Error($"SchoolController.Delete异常", ex);
                return Error(ex.Message);
            }
        }
    }
}
using Instart.Web2.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Instart.Service;
using Instart.Service.Base;
using System.Threading.Tasks;
using Instart.Models;
using Instart.Common;
using Instart.Web2.Models;

namespace Instart.Web2.Areas.Manage.Controllers
{
    [AdminValidation]
    public class DivisionController : ManageControllerBase
    {
        IDivisionService _divisionService = AutofacService.Resolve<IDivisionService>();

        public DivisionController()
        {
            base.AddDisposableObject(_divisionService);
        }

        public ActionResult Index(int page = 1, string keyword = null)
        {
            int pageSize = 10;
            var list = _divisionService.GetListAsync(page, pageSize, keyword);
            ViewBag.Total = list.Total;
            ViewBag.PageIndex = page;
            ViewBag.TotalPages = Math.Ceiling(list.Total * 1.0 / pageSize);
            ViewBag.Keyword = keyword;
            return View(list.Data);
        }

        public ActionResult Edit(int id = 0)
        {
            Division model = new Division();
            string action = "添加学部";

            if (id > 0)
            {
                model = _divisionService.GetByIdAsync(id);
                action = "修改学部";
            }

            ViewBag.Action = action;
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult AddOrUpdate(Division model, List<HttpPostedFileBase> imgs)
        {
            try
            {
                string msg = this.Validate(model, false);

                if (!string.IsNullOrEmpty(msg))
                {
                    return Error(msg);
                }

                if (model.Id > 0)
                {
                    return Json(new ResultBase
                    {
                        success = _divisionService.UpdateAsync(model)
                    });
                }
                else
                {
                    return Json(new ResultBase
                    {
                        success = _divisionService.InsertAsync(model)
                    });
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("DivisionController.设置异常", ex);
                return Error(ex.Message);
            }
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                return Json(new ResultBase
                {
                    success = _divisionService.DeleteAsync(id)
                });
            }
            catch (Exception ex)
            {
                LogHelper.Error("DivisionController.Delete异常", ex);
                return Error(ex.Message);
            }
        }

        [NonAction]
        private string Validate(Division model, bool isUpdate = false)
        {
            if (model == null)
            {
                return "参数错误。";
            }

            if (isUpdate && model.Id <= 0)
            {
                return "Id不正确。";
            }

            if (string.IsNullOrEmpty(model.Name))
            {
                return "学部名称不能为空。";
            }

            return string.Empty;
        }
    }
}
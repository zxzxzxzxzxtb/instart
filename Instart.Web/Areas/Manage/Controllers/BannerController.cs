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
    public class BannerController : ManageControllerBase
    {
        IBannerService _bannserService = AutofacService.Resolve<IBannerService>();

        public BannerController()
        {
            base.AddDisposableObject(_bannserService);
        }

        public async Task<ActionResult> Index(int page = 1, string title = null)
        {
            int pageSize = 10;
            var list = await _bannserService.GetListAsync(page, pageSize, title);
            ViewBag.Total = list.Total;
            ViewBag.PageIndex = page;
            ViewBag.TotalPages = Math.Ceiling(list.Total * 1.0 / pageSize);
            return View(list.Data);
        }
        
        [HttpPost]
        public async Task<JsonResult> Insert(Banner model)
        {
            try
            {
                string msg = this.Validate(model, false);

                if (!string.IsNullOrEmpty(msg))
                {
                    return Error(msg);
                }
                
                return Json(new ResultBase
                {
                    success = await _bannserService.UpdateAsync(model)
                });
            }
            catch(Exception ex)
            {
                LogHelper.Error($"BannerController.Insert异常", ex);
                return Error(ex.Message);
            }
        }

        [HttpPost]
        public async Task<JsonResult> Update(Banner model)
        {
            try
            {
                string msg = this.Validate(model, true);
                if (!string.IsNullOrEmpty(msg))
                {
                    return Error(msg);
                }


                return Json(new ResultBase
                {
                    success = await _bannserService.UpdateAsync(model)
                });
            }
            catch (Exception ex)
            {
                LogHelper.Error($"BannerController.Update异常", ex);
                return Error(ex.Message);
            }
        }

        [NonAction]
        private string Validate(Banner model, bool isUpdate = false)
        {
            if (model == null)
            {
                return "参数错误";
            }

            if (isUpdate && model.Id <= 0)
            {
                return "Id不正确";
            }

            if (string.IsNullOrEmpty(model.Title))
            {
                return "标题不能为空";
            }

            if (string.IsNullOrEmpty(model.ImageUrl))
            {
                return "图片不能为空";
            }

            return string.Empty;
        }
    }
}
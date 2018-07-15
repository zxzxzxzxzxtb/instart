using Instart.Common;
using Instart.Models;
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
    public class BannerController : ManageControllerBase
    {
        IBannerService _bannserService = AutofacService.Resolve<IBannerService>();

        public BannerController()
        {
            base.AddDisposableObject(_bannserService);
        }

        public async Task<ActionResult> Index(int page = 1, string keyword = null)
        {
            int pageSize = 10;
            var list = await _bannserService.GetListAsync(page, pageSize, keyword);
            ViewBag.Total = list.Total;
            ViewBag.PageIndex = page;
            ViewBag.TotalPages = Math.Ceiling(list.Total * 1.0 / pageSize);
            ViewBag.Keyword = keyword;
            return View(list.Data);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id = 0)
        {
            Banner model = new Banner();
            string action = "添加轮播";

            if (id > 0)
            {
                model = await _bannserService.GetByIdAsync(id);
                action = "修改轮播";
            }

            ViewBag.Action = action;
            ViewBag.TypeList = EnumberHelper.EnumToList<EnumBannerType>();
            ViewBag.PosList = EnumberHelper.EnumToList<EnumBannerPos>();
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> Set(Banner model)
        {
            if (model == null)
            {
                return Error("参数错误");
            }

            if (string.IsNullOrEmpty(model.Title))
            {
                return Error("标题不能为空");
            }

            if(string.IsNullOrEmpty(model.ImageUrl) && Request.Files["fileImage"] == null)
            {
                return Error("图片不能为空");
            }

            model.Title = model.Title.Trim();

            var imageFile = Request.Files["fileImage"];
            var videoFile = Request.Files["fileVideo"];

            if (imageFile != null)
            {
                string uploadResult = UploadHelper.Process(imageFile.FileName, imageFile.InputStream);
                if (!string.IsNullOrEmpty(uploadResult))
                {
                    model.ImageUrl = uploadResult;
                }
            }

            if (videoFile != null)
            {
                string uploadResult = UploadHelper.Process(videoFile.FileName, videoFile.InputStream);
                if (!string.IsNullOrEmpty(uploadResult))
                {
                    model.VideoUrl = uploadResult;
                }
            }

            var result = new ResultBase();

            if (model.Id > 0)
            {
                result.success = await _bannserService.UpdateAsync(model);
            }
            else
            {
                result.success = await _bannserService.InsertAsync(model);
            }

            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            if (id <= 0)
            {
                return Error("id错误");
            }

            try
            {
                return Json(new ResultBase
                {
                    success = await _bannserService.DeleteAsync(id)
                });
            }
            catch (Exception ex)
            {
                LogHelper.Error($"BannerController.Delete异常", ex);
                return Error(ex.Message);
            }
        }

        [HttpPost]
        public async Task<JsonResult> SetShow(int id, bool isShow)
        {
            if (id <= 0)
            {
                return Error("id错误");
            }

            try
            {
                return Json(new ResultBase
                {
                    success = await _bannserService.SetShowAsync(id, isShow)
                });
            }
            catch (Exception ex)
            {
                LogHelper.Error($"SchoolController.SetShow异常", ex);
                return Error(ex.Message);
            }
        }
    }
}
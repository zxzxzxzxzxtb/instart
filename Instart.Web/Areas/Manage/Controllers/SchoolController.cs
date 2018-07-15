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

            if (id > 0)
            {
                model = await _schoolService.GetByIdAsync(id);
                action = "修改学校";
            }

            ViewBag.Action = action;
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> Set(School model)
        {
            if (model == null)
            {
                return Error("参数错误");
            }

            if (string.IsNullOrEmpty(model.Name))
            {
                return Error("学校名称不能为空");
            }

            model.Name = model.Name.Trim();

            var avatarFile = Request.Files["fileAvatar"];
            var logoFile = Request.Files["fileLogo"];

            if (avatarFile != null)
            {
                string uploadResult = UploadHelper.Process(avatarFile.FileName, avatarFile.InputStream);
                if (!string.IsNullOrEmpty(uploadResult))
                {
                    model.Avatar = uploadResult;
                }
            }

            if (logoFile != null)
            {
                string uploadResult = UploadHelper.Process(logoFile.FileName, logoFile.InputStream);
                if (!string.IsNullOrEmpty(uploadResult))
                {
                    model.Logo = uploadResult;
                }
            }

            var result = new ResultBase();

            if (model.Id > 0)
            {
                result.success = await _schoolService.UpdateAsync(model);
            }
            else
            {
                result.success = await _schoolService.InsertAsync(model);
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
                    success = await _schoolService.DeleteAsync(id)
                });
            }
            catch (Exception ex)
            {
                LogHelper.Error($"SchoolController.Delete异常", ex);
                return Error(ex.Message);
            }
        }

        [HttpPost]
        public async Task<JsonResult> SetRecommend(int id, bool isRecommend)
        {
            if (id <= 0)
            {
                return Error("id错误");
            }

            try
            {
                return Json(new ResultBase
                {
                    success = await _schoolService.SetRecommendAsync(id, isRecommend)
                });
            }
            catch (Exception ex)
            {
                LogHelper.Error($"SchoolController.SetRecommend异常", ex);
                return Error(ex.Message);
            }
        }
    }
}
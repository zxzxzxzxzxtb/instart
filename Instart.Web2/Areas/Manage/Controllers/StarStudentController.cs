using Instart.Common;
using Instart.Models;
using Instart.Service;
using Instart.Service.Base;
using Instart.Web2.Attributes;
using Instart.Web2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Instart.Web2.Areas.Manage.Controllers
{
    [AdminValidation]
    public class StarStudentController : ManageControllerBase
    {
        IStarStudentService _starStudentService = AutofacService.Resolve<IStarStudentService>();

        public StarStudentController()
        {
            base.AddDisposableObject(_starStudentService);
        }

        public ActionResult Index(int page = 1, string keyword = null)
        {
            int pageSize = 10;
            var list = _starStudentService.GetListAsync(page, pageSize, keyword);
            ViewBag.Total = list.Total;
            ViewBag.PageIndex = page;
            ViewBag.TotalPages = Math.Ceiling(list.Total * 1.0 / pageSize);
            ViewBag.Keyword = keyword;
            return View(list.Data);
        }

        public ActionResult Edit(int id = 0)
        {
            StarStudent model = new StarStudent();
            string action = "添加专访视频";

            if (id > 0)
            {
                model = _starStudentService.GetByIdAsync(id);
                action = "修改专访视频";
            }
            ViewBag.Action = action;
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [Operation("设置学员专访")]
        public JsonResult Set(StarStudent model)
        {
            if (model == null)
            {
                return Error("参数错误。");
            }

            if (string.IsNullOrEmpty(model.Title))
            {
                return Error("专业名称不能为空。");
            }

            var fileImg = Request.Files["fileImg"];
            if (fileImg != null)
            {
                string uploadResult = UploadHelper.Process(fileImg.FileName, fileImg.InputStream);
                if (!string.IsNullOrEmpty(uploadResult))
                {
                    model.ImgUrl = uploadResult;
                }
            }

            var fileVideo = Request.Files["fileVideo"];
            if (fileVideo != null)
            {
                string uploadResult = UploadHelper.Process(fileVideo.FileName, fileVideo.InputStream);
                if (!string.IsNullOrEmpty(uploadResult))
                {
                    model.VideoUrl = uploadResult;
                }
            }
            if (String.IsNullOrEmpty(model.VideoUrl))
            {
                return Error("视频不能为空。");
            }
            var result = new ResultBase();

            if (model.Id > 0)
            {
                result.success = _starStudentService.UpdateAsync(model);
            }
            else
            {
                result.success = _starStudentService.InsertAsync(model);
            }

            return Json(result);
        }

        [HttpPost]
        [Operation("删除学员专访")]
        public JsonResult Delete(int id)
        {
            try
            {
                return Json(new ResultBase
                {
                    success = _starStudentService.DeleteAsync(id)
                });
            }
            catch (Exception ex)
            {
                LogHelper.Error("StarStudentController.Delete异常", ex);
                return Error(ex.Message);
            }
        }
    }
}
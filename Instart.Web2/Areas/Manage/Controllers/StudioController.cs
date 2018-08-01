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
using System.IO;

namespace Instart.Web2.Areas.Manage.Controllers
{
    [AdminValidation]
    public class StudioController : ManageControllerBase
    {
        IStudioService _studioService = AutofacService.Resolve<IStudioService>();

        public StudioController()
        {
            base.AddDisposableObject(_studioService);
        }

        public ActionResult Index()
        {
            Studio model = _studioService.GetInfoAsync();
            if (model == null) model = new Studio();
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [Operation("设置24小时工作")]
        public JsonResult Set(Studio model)
        {
            if (model == null)
            {
                return Error("参数错误。");
            }

            var fileStudio = Request.Files["fileStudio"];

            if (fileStudio != null)
            {
                string uploadResult = UploadHelper.Process(fileStudio.FileName, fileStudio.InputStream);
                if (!string.IsNullOrEmpty(uploadResult))
                {
                    model.ImgUrl = uploadResult;
                }
            }

            var result = new ResultBase();

            int count = _studioService.GetCountAsync();
            if (count > 0)
            {
                result.success = _studioService.UpdateAsync(model);
            }
            else
            {
                result.success = _studioService.InsertAsync(model);
            }

            return Json(result);
        }

        public ActionResult ImgIndex()
        {
            ViewBag.ImgList = _studioService.GetImgsAsync();
            return View();
        }

        [HttpPost]
        [Operation("设置24小时工作室图片")]
        public JsonResult SetImg()
        {
            var studioImg = Request.Files["studioImg"];

            if (studioImg == null)
            {
                return Error("请选择图片。");
            }
            string uploadResult = UploadHelper.Process(studioImg.FileName, studioImg.InputStream);
            if (string.IsNullOrEmpty(uploadResult))
            {
                return Error("请选择图片。");
            }

            var result = new ResultBase();
            result.success = _studioService.InsertImgAsync(uploadResult);

            return Json(result);
        }

        [HttpPost]
        [Operation("删除24小时工作室图片")]
        public JsonResult DeleteImg(int id)
        {
            try
            {
                return Json(new ResultBase
                {
                    success = _studioService.DeleteImgAsync(id)
                });
            }
            catch (Exception ex)
            {
                LogHelper.Error("StudioController.DeleteImg异常", ex);
                return Error(ex.Message);
            }
        }
    }
}
using Instart.Web.Attributes;
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
using Instart.Web.Models;
using System.IO;

namespace Instart.Web.Areas.Manage.Controllers
{
    [AdminValidation]
    public class AboutController : ManageControllerBase
    {
        IAboutInstartService _aboutInstartService = AutofacService.Resolve<IAboutInstartService>();

        public AboutController()
        {
            base.AddDisposableObject(_aboutInstartService);
        }

        public async Task<ActionResult> Index()
        {
            var list = await _aboutInstartService.GetInfoAsync();
            return View(list);
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<JsonResult> Set(AboutInstart model)
        {
            if (model == null)
            {
                return Error("参数错误。");
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
            var result = new ResultBase();

            int count = await _aboutInstartService.GetCountAsync();
            if (count > 0)
            {
                result.success = await _aboutInstartService.UpdateAsync(model);
            }
            else
            {
                result.success = await _aboutInstartService.InsertAsync(model);
            }

            return Json(result);
        }
    }
}
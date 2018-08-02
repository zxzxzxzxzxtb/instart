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
    public class AboutController : ManageControllerBase
    {
        IAboutInstartService _aboutInstartService = AutofacService.Resolve<IAboutInstartService>();

        public AboutController()
        {
            base.AddDisposableObject(_aboutInstartService);
        }

        public ActionResult Index()
        {
            AboutInstart model = _aboutInstartService.GetInfoAsync();
            if (model == null) model = new AboutInstart();
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [Operation("设置关于我们")]
        public JsonResult Set(AboutInstart model)
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
            var filePreToPro = Request.Files["filePreToPro"];
            if (filePreToPro != null)
            {
                string uploadResult = UploadHelper.Process(filePreToPro.FileName, filePreToPro.InputStream);
                if (!string.IsNullOrEmpty(uploadResult))
                {
                    model.PreToProImg = uploadResult;
                }
            }
            var fileDivision = Request.Files["fileDivision"];
            if (fileDivision != null)
            {
                string uploadResult = UploadHelper.Process(fileDivision.FileName, fileDivision.InputStream);
                if (!string.IsNullOrEmpty(uploadResult))
                {
                    model.DivisionImg = uploadResult;
                }
            }
            var filePassLearning = Request.Files["filePassLearning"];
            if (filePassLearning != null)
            {
                string uploadResult = UploadHelper.Process(filePassLearning.FileName, filePassLearning.InputStream);
                if (!string.IsNullOrEmpty(uploadResult))
                {
                    model.PassLearningImg = uploadResult;
                }
            }
            var fileWorkShop = Request.Files["fileWorkShop"];
            if (fileWorkShop != null)
            {
                string uploadResult = UploadHelper.Process(fileWorkShop.FileName, fileWorkShop.InputStream);
                if (!string.IsNullOrEmpty(uploadResult))
                {
                    model.WorkShopImg = uploadResult;
                }
            }
            var fileStudio = Request.Files["fileStudio"];
            if (fileStudio != null)
            {
                string uploadResult = UploadHelper.Process(fileStudio.FileName, fileStudio.InputStream);
                if (!string.IsNullOrEmpty(uploadResult))
                {
                    model.StudioImg = uploadResult;
                }
            }
            var fileCompany = Request.Files["fileCompany"];
            if (fileCompany != null)
            {
                string uploadResult = UploadHelper.Process(fileCompany.FileName, fileCompany.InputStream);
                if (!string.IsNullOrEmpty(uploadResult))
                {
                    model.CompanyImg = uploadResult;
                }
            }
            var fileActor = Request.Files["fileActor"];
            if (fileActor != null)
            {
                string uploadResult = UploadHelper.Process(fileActor.FileName, fileActor.InputStream);
                if (!string.IsNullOrEmpty(uploadResult))
                {
                    model.ActorImg = uploadResult;
                }
            }
            var filePrograms = Request.Files["filePrograms"];
            if (filePrograms != null)
            {
                string uploadResult = UploadHelper.Process(filePrograms.FileName, filePrograms.InputStream);
                if (!string.IsNullOrEmpty(uploadResult))
                {
                    model.ProgramsImg = uploadResult;
                }
            }
            var result = new ResultBase();

            int count = _aboutInstartService.GetCountAsync();
            if (count > 0)
            {
                result.success = _aboutInstartService.UpdateAsync(model);
            }
            else
            {
                result.success = _aboutInstartService.InsertAsync(model);
            }

            return Json(result);
        }
    }
}
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
        public async Task<JsonResult> AddOrUpdate(AboutInstart model)
        {
            Stream uploadStream = null;
            FileStream fs = null;
            try
            {
                string msg = this.Validate(model, false);

                if (!string.IsNullOrEmpty(msg))
                {
                    return Error(msg);
                }
                //文件上传，一次上传1M的数据，防止出现大文件无法上传
                HttpPostedFileBase postFileBase = Request.Files["InstartVideo"];
                if (postFileBase == null || postFileBase.ContentLength == 0)
                {
                    return Error("团队视频不能为空。");
                }
                uploadStream = postFileBase.InputStream;
                int bufferLen = 1024;
                byte[] buffer = new byte[bufferLen];
                int contentLen = 0;

                string fileName = Path.GetFileName(postFileBase.FileName);
                string baseUrl = Server.MapPath("/");
                string uploadPath = baseUrl + @"\Content\Videos\AboutInstart\";
                fs = new FileStream(uploadPath + fileName, FileMode.Create, FileAccess.ReadWrite);

                while ((contentLen = uploadStream.Read(buffer, 0, bufferLen)) != 0)
                {
                    fs.Write(buffer, 0, bufferLen);
                    fs.Flush();
                }

                //保存页面数据，上传的文件只保存路径
                string videogUrl = "/Content/Videos/AboutInstart/" + fileName;
                model.VideoUrl = videogUrl;

                int count = await _aboutInstartService.GetCountAsync();
                if (count > 0)
                {
                    await _aboutInstartService.UpdateAsync(model);
                }
                else
                {
                    await _aboutInstartService.InsertAsync(model);
                }
            }
            catch (Exception ex)
            {
                ex.StackTrace.ToString();
            }
            finally
            {
                if (null != fs)
                {
                    fs.Close();
                }
                if (null != uploadStream)
                {
                    uploadStream.Close();
                }
            }
            return Json(new ResultBase
            {
                success = true
            });
        }

        [NonAction]
        private string Validate(AboutInstart model, bool isUpdate = false)
        {
            if (model == null)
            {
                return "参数错误。";
            }

            if (string.IsNullOrEmpty(model.Introduce))
            {
                return "关于一沙不能为空。";
            }

            return string.Empty;
        }
    }
}
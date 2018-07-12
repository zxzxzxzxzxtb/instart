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
    public class ContactController : ManageControllerBase
    {
        IContactService _contactService = AutofacService.Resolve<IContactService>();

        public ContactController()
        {
            base.AddDisposableObject(_contactService);
        }

        public async Task<ActionResult> Index()
        {
            var list = await _contactService.GetInfoAsync();
            return View(list);
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<JsonResult> AddOrUpdate(Contact model)
        {
            Stream uploadStream = null;
            FileStream fs = null;
            try
            {
                //文件上传，一次上传1M的数据，防止出现大文件无法上传
                HttpPostedFileBase postFileBase = Request.Files["InstartVideo"];
                if (postFileBase != null && postFileBase.ContentLength != 0)
                {
                    uploadStream = postFileBase.InputStream;
                    int bufferLen = 1024;
                    byte[] buffer = new byte[bufferLen];
                    int contentLen = 0;

                    string fileName = Path.GetFileName(postFileBase.FileName);
                    string baseUrl = Server.MapPath("/");
                    string uploadPath = baseUrl + @"\Content\Images\Contact\";
                    fs = new FileStream(uploadPath + fileName, FileMode.Create, FileAccess.ReadWrite);

                    while ((contentLen = uploadStream.Read(buffer, 0, bufferLen)) != 0)
                    {
                        fs.Write(buffer, 0, bufferLen);
                        fs.Flush();
                    }

                    //保存页面数据，上传的文件只保存路径
                    string qrcodeUrl = "/Content/Images/Contact/" + fileName;
                    model.Qrcode = qrcodeUrl;
                }
                int count = await _contactService.GetCountAsync();
                if (count > 0)
                {
                    await _contactService.UpdateAsync(model);
                }
                else
                {
                    await _contactService.InsertAsync(model);
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
    }
}
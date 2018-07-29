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
    public class ContactController : ManageControllerBase
    {
        IContactService _contactService = AutofacService.Resolve<IContactService>();

        public ContactController()
        {
            base.AddDisposableObject(_contactService);
        }

        public ActionResult Index()
        {
            Contact model = _contactService.GetInfoAsync();
            if (model == null) model = new Contact();
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [Operation("设置联系方式")]
        public JsonResult Set(Contact model)
        {
            var fileQrcode = Request.Files["fileQrcode"];

            if (fileQrcode != null)
            {
                string uploadResult = UploadHelper.Process(fileQrcode.FileName, fileQrcode.InputStream);
                if (!string.IsNullOrEmpty(uploadResult))
                {
                    model.Qrcode = uploadResult;
                }
            }
            var result = new ResultBase();

            int count = _contactService.GetCountAsync();
            if (count > 0)
            {
                result.success = _contactService.UpdateAsync(model);
            }
            else
            {
                result.success = _contactService.InsertAsync(model);
            }

            return Json(result);
        }
    }
}
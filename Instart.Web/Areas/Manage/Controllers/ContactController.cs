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
        public async Task<JsonResult> Set(Contact model)
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

            int count = await _contactService.GetCountAsync();
            if (count > 0)
            {
                result.success = await _contactService.UpdateAsync(model);
            }
            else
            {
                result.success = await _contactService.InsertAsync(model);
            }

            return Json(result);
        }
    }
}
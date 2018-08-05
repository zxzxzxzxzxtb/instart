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
    public class CopysController : ManageControllerBase
    {
        ICopysService _copysService = AutofacService.Resolve<ICopysService>();

        public CopysController()
        {
            base.AddDisposableObject(_copysService);
        }

        public ActionResult Index()
        {
            Copys model = _copysService.GetInfoAsync();
            if (model == null) model = new Copys();
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [Operation("设置文案")]
        public JsonResult Set(Copys model)
        {
            if (model == null)
            {
                return Error("参数错误。");
            }

            var result = new ResultBase();

            int count = _copysService.GetCountAsync();
            if (count > 0)
            {
                result.success = _copysService.UpdateAsync(model);
            }
            else
            {
                result.success = _copysService.InsertAsync(model);
            }

            return Json(result);
        }
    }
}
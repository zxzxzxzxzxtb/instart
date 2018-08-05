using Instart.Models;
using Instart.Service;
using Instart.Service.Base;
using Instart.Web2.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Instart.Web2.Areas.Manage.Controllers
{
    [AdminValidation]
    public class PassLearnController : ManageControllerBase
    {
         IAboutInstartService _aboutInstartService = AutofacService.Resolve<IAboutInstartService>();

        public PassLearnController()
        {
            base.AddDisposableObject(_aboutInstartService);
        }

        public ActionResult Index()
        {
            AboutInstart model = _aboutInstartService.GetInfoAsync();
            if (model == null)
            {
                model = new AboutInstart();
            }
            ViewBag.Learning = model.PassLearning;
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [Operation("设置跨学科教学")]
        public JsonResult Set(string content)
        {
            _aboutInstartService.UpdatePassLearningAsync(content);
            return Success();
        }
    }
}

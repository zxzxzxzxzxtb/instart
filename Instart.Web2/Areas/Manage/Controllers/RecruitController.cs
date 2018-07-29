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
    public class RecruitController : ManageControllerBase
    {
        IRecruitService _recruitService = AutofacService.Resolve<IRecruitService>();

        public RecruitController()
        {
            base.AddDisposableObject(_recruitService);
        }

        public ActionResult Index()
        {
            Recruit model = _recruitService.GetInfoAsync();
            if (model == null) model = new Recruit();
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Set(Recruit model)
        {
            var result = new ResultBase();
            int count = _recruitService.GetCountAsync();
            if (count > 0)
            {
                result.success = _recruitService.UpdateAsync(model);
            }
            else
            {
                result.success = _recruitService.InsertAsync(model);
            }

            return Json(result);
        }
    }
}
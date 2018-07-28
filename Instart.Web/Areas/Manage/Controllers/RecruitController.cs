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
    public class RecruitController : ManageControllerBase
    {
        IRecruitService _recruitService = AutofacService.Resolve<IRecruitService>();

        public RecruitController()
        {
            base.AddDisposableObject(_recruitService);
        }

        public async Task<ActionResult> Index()
        {
            Recruit model = await _recruitService.GetInfoAsync();
            if (model == null) model = new Recruit();
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<JsonResult> Set(Recruit model)
        {
            var result = new ResultBase();
            int count = await _recruitService.GetCountAsync();
            if (count > 0)
            {
                result.success = await _recruitService.UpdateAsync(model);
            }
            else
            {
                result.success = await _recruitService.InsertAsync(model);
            }

            return Json(result);
        }
    }
}
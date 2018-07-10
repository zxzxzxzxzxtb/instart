using Instart.Common;
using Instart.Service;
using Instart.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Instart.Web.Controllers
{
    public class HomeController : ControllerBase
    {
        IPartnerService _partnerService = AutofacService.Resolve<IPartnerService>();
        ISchoolService _schoolService = AutofacService.Resolve<ISchoolService>();

        public HomeController() {
            this.AddDisposableObject(_partnerService);
        }

        public async Task<ActionResult> Index() {
            ViewBag.PartnerList = (await _partnerService.GetListAsync(14)) ?? new List<Instart.Models.Partner>();
            ViewBag.SchoolList = (await _schoolService.GetRecommendListAsync(10)) ?? new List<Instart.Models.School>();
            return View();
        }
    }
}
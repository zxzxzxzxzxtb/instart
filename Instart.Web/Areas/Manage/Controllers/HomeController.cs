using Instart.Service;
using Instart.Service.Base;
using Instart.Web.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Instart.Web.Areas.Manage.Controllers
{
    [AdminValidation]
    public class HomeController : ManageControllerBase
    {
        ICourseApplyService _courseApplyService = AutofacService.Resolve<ICourseApplyService>();
        ISchoolApplyService _schoolApplyService = AutofacService.Resolve<ISchoolApplyService>();
        IStatisticsService _statisticsService = AutofacService.Resolve<IStatisticsService>();

        public HomeController()
        {
            this.AddDisposableObject(_courseApplyService);
            this.AddDisposableObject(_schoolApplyService);
            this.AddDisposableObject(_statisticsService);
        }

        public async Task<ActionResult> Index()
        {
            ViewBag.CourseApplyList = (await _courseApplyService.GetTopListAsync(5)) ?? new List<Instart.Models.CourseApply>();
            ViewBag.SchoolApplyList = (await _schoolApplyService.GetTopListAsync(5)) ?? new List<Instart.Models.SchoolApply>();
            ViewBag.Statistics = (await _statisticsService.GetAsync()) ?? new Instart.Models.Statistics();
            return View();
        }
    }
}
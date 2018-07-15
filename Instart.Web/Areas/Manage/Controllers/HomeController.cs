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

        public HomeController()
        {
            this.AddDisposableObject(_courseApplyService);
            this.AddDisposableObject(_schoolApplyService);
        }

        public async Task<ActionResult> Index()
        {
            ViewBag.CourseApplyList = (await _courseApplyService.GetTopListAsync(5)) ?? new List<Instart.Models.CourseApply>();
            ViewBag.SchoolApplyList = (await _schoolApplyService.GetTopListAsync(5)) ?? new List<Instart.Models.SchoolApply>();
            return View();
        }
    }
}
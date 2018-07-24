using Instart.Service;
using Instart.Service.Base;
using Instart.Web.Attributes;
using Instart.Web.Models;
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
        IUserService _userService = AutofacService.Resolve<IUserService>();

        public HomeController()
        {
            this.AddDisposableObject(_courseApplyService);
            this.AddDisposableObject(_schoolApplyService);
            this.AddDisposableObject(_statisticsService);
            this.AddDisposableObject(_userService);
        }

        public async Task<ActionResult> Index()
        {
            ViewBag.CourseApplyList = (await _courseApplyService.GetTopListAsync(5)) ?? new List<Instart.Models.CourseApply>();
            ViewBag.SchoolApplyList = (await _schoolApplyService.GetTopListAsync(5)) ?? new List<Instart.Models.SchoolApply>();
            ViewBag.Statistics = (await _statisticsService.GetAsync()) ?? new Instart.Models.Statistics();
            return View();
        }

        public async Task<ActionResult> EditInfo()
        {
            Instart.Models.User model;
            if (Session[WebAppSettings.SessionName] != null)
            {
                LoginUser user = (LoginUser)Session[WebAppSettings.SessionName];
                model = (await _userService.GetByIdAsync(user.UserId)) ?? new Instart.Models.User();
            }
            else
            {
                model = new Instart.Models.User();
            }
            return View(model);
        }

        public async Task<ActionResult> UpdatePwd()
        {
            int id = 0;
            if (Session[WebAppSettings.SessionName] != null)
            {
                LoginUser user = (LoginUser)Session[WebAppSettings.SessionName];
                id = user.UserId;
            }
            ViewBag.Id = id;
            return View();
        }
    }
}
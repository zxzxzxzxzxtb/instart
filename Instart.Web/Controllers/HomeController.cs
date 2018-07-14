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
        ITeacherService _teacherService = AutofacService.Resolve<ITeacherService>();
        IStudentService _studentService = AutofacService.Resolve<IStudentService>();
        ICourseService _courseService = AutofacService.Resolve<ICourseService>();
        IBannerService _bannerService = AutofacService.Resolve<IBannerService>();

        public HomeController() {
            this.AddDisposableObject(_partnerService);
            this.AddDisposableObject(_schoolService);
            this.AddDisposableObject(_teacherService);
            this.AddDisposableObject(_studentService);
            this.AddDisposableObject(_courseService);
            this.AddDisposableObject(_bannerService);
        }

        public async Task<ActionResult> Index() {
            ViewBag.PartnerList = (await _partnerService.GetRecommendListAsync(14)) ?? new List<Instart.Models.Partner>();
            ViewBag.SchoolList = (await _schoolService.GetRecommendListAsync(10)) ?? new List<Instart.Models.School>();
            ViewBag.TeacherList = (await _teacherService.GetRecommendListAsync(8)) ?? new List<Instart.Models.Teacher>();
            ViewBag.StudentList = (await _studentService.GetRecommendListAsync(8)) ?? new List<Instart.Models.Student>();
            ViewBag.CourseList = (await _courseService.GetRecommendListAsync(3)) ?? new List<Instart.Models.Course>();
            ViewBag.BannerList = (await _bannerService.GetBannerListByPosAsync(Instart.Models.Enums.EnumBannerPos.Index)) ?? new List<Instart.Models.Banner>();
            return View();
        }
    }
}
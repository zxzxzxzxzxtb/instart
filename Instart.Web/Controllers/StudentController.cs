using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Instart.Service;
using Instart.Service.Base;

namespace Instart.Web.Controllers
{
    /// <summary>
    /// 成功学员
    /// </summary>
    public class StudentController : ControllerBase
    {
        IStudentService _studentService = AutofacService.Resolve<IStudentService>();
        ISchoolService _schoolService = AutofacService.Resolve<ISchoolService>();
        IBannerService _bannerService = AutofacService.Resolve<IBannerService>();
        ICourseService _courseService = AutofacService.Resolve<ICourseService>();

        public StudentController()
        {
            this.AddDisposableObject(_studentService);
            this.AddDisposableObject(_schoolService);
            this.AddDisposableObject(_bannerService);
            this.AddDisposableObject(_courseService);
        }

        public async Task<ActionResult> Index() {
            ViewBag.StudentList = (await _studentService.GetAllAsync()) ?? new List<Instart.Models.Student>();
            ViewBag.VideoList = (await _studentService.GetStarStudentsAsync()) ?? new List<Instart.Models.Student>();
            ViewBag.SchoolList = (await _schoolService.GetAllAsync()) ?? new List<Instart.Models.School>();
            ViewBag.CourseList = (await _courseService.GetRecommendListAsync(3)) ?? new List<Instart.Models.Course>();
            ViewBag.BannerList = (await _bannerService.GetBannerListByPosAsync(Instart.Models.Enums.EnumBannerPos.Student)) ?? new List<Instart.Models.Banner>();
            return View();
        }
    }
}
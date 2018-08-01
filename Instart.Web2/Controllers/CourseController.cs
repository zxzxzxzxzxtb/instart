using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Instart.Models;
using Instart.Service;
using Instart.Service.Base;

namespace Instart.Web2.Controllers
{
    /// <summary>
    /// 课程
    /// </summary>
    public class CourseController : ControllerBase
    {
        ICourseService _courseService = AutofacService.Resolve<ICourseService>();
        IBannerService _bannerService = AutofacService.Resolve<IBannerService>();
        IStudentService _studentService = AutofacService.Resolve<IStudentService>();
        IWorksService _worksService = AutofacService.Resolve<IWorksService>();

        public CourseController()
        {
            this.AddDisposableObject(_courseService);
            this.AddDisposableObject(_bannerService);
            this.AddDisposableObject(_studentService);
            this.AddDisposableObject(_worksService);
        }

        public ActionResult Index(int studentId = -1)
        {
            IEnumerable<Course> courseList;
            if (studentId == -1)
            {
                courseList = _courseService.GetAllAsync() ?? new List<Course>();
            }
            else 
            {
                courseList = _courseService.GetAllByStudentAsync(studentId) ?? new List<Course>();
            }

            ViewBag.CourseList = courseList;
            ViewBag.BannerList = _bannerService.GetBannerListByPosAsync(Instart.Models.Enums.EnumBannerPos.Course) ?? new List<Instart.Models.Banner>();
            ViewBag.StudentList = _studentService.GetAllAsync() ?? new List<Instart.Models.Student>();
            return View();
        }

        public ActionResult Details(int id = 0)
        {
            Course course = _courseService.GetByIdAsync(id);
            ViewBag.WorkList = _worksService.GetListByMajorIdAsync(id, 3) ?? new List<Instart.Models.Works>();
            ViewBag.BannerList = _bannerService.GetBannerListByPosAsync(Instart.Models.Enums.EnumBannerPos.Teacher) ?? new List<Instart.Models.Banner>();
            ViewBag.StudentList = _studentService.GetListByCourseAsync(id) ?? new List<Instart.Models.Student>();
            return View(course ?? new Course());
        }
    }
}
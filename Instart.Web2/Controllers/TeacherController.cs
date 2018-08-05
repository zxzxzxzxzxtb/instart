using Instart.Service;
using Instart.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Instart.Web2.Controllers
{
    /// <summary>
    /// 艺术导师
    /// </summary>
    public class TeacherController : ControllerBase
    {
        ITeacherService _teacherService = AutofacService.Resolve<ITeacherService>();
        IDivisionService _divisionService = AutofacService.Resolve<IDivisionService>();
        IStudentService _studentService = AutofacService.Resolve<IStudentService>();

        public TeacherController()
        {
            this.AddDisposableObject(_teacherService);
            this.AddDisposableObject(_divisionService);
            this.AddDisposableObject(_studentService);
        }

        public  ActionResult Index(int id = 0)
        {
            var divisionList =  _divisionService.GetAllAsync();

            if (divisionList == null || divisionList.Count() == 0)
            {
                throw new Exception("请先创建学部");
            }

            if(id == 0)
            {
                id = divisionList.First().Id;
            }

            ViewBag.DivisionList = divisionList;
            ViewBag.DivisionId = id;
            return View();
        }

        [HttpPost]
        public  JsonResult GetTeacherList(int divisionId, int pageIndex, int pageSize = 8)
        {
            var result =  _teacherService.GetListByDivsionAsync(divisionId, pageIndex, pageSize);
            return Success(data: new
            {
                total = result.Total,
                pageSize = pageSize,
                totalPage = (int)Math.Ceiling(result.Total * 1.0 / pageSize),
                list = result.Data
            });
        }

        public  ActionResult Details(int id)
        {
            var teacher =  _teacherService.GetByIdAsync(id);
            if(teacher == null)
            {
                throw new Exception("导师不存在");
            }

            ViewBag.CourseList = _teacherService.GetCoursesByIdAsync(id) ?? new List<Instart.Models.Course>();
            ViewBag.StudentList = _studentService.GetListByTeacherAsync(id) ?? new List<Instart.Models.Student>();
            return View(teacher);
        }
    }
}
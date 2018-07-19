using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Instart.Models;
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
            IEnumerable<Student> studentList = (await _studentService.GetAllAsync()) ?? new List<Student>();
            IEnumerable<School> schoolList = (await _schoolService.GetAllAsync()) ?? new List<School>();

            //计算录取比例
            foreach (School school in schoolList)
            {
                int count = 0;
                foreach (Student student in studentList)
                {
                    if (student.SchoolId == school.Id)
                    {
                        count++;
                    }
                }
                school.AcceptRate = "0";
                if (schoolList.Count() > 0)
                {
                    decimal rate = count.ToDecimal() / schoolList.Count().ToDecimal();
                    Console.Write(rate);
                    school.AcceptRate = (rate * 100).ToString("f2");
                }
            }

            //一行4个学员
            List<List<Student>> studentMap = new List<List<Student>>();
            int studentIndex = 1;
            List<Student> slist = new List<Student>();
            foreach (Student student in studentList)
            {
                slist.Add(student);
                if (studentIndex % 2 == 0)
                {
                    studentMap.Add(new List<Student>(slist.ToArray()));
                    slist.Clear();
                }
                studentIndex++;
            }
            if (slist.Count > 0)
            {
                studentMap.Add(slist);
            }

            //一行6个学校
            List<List<School>> schoolMap = new List<List<School>>();
            int schoolIndex = 1;
            List<School> tlist = new List<School>();
            foreach (School school in schoolList)
            {
                tlist.Add(school);
                if (schoolIndex % 6 == 0)
                {
                    schoolMap.Add(new List<School>(tlist.ToArray()));
                    tlist.Clear();
                }
                schoolIndex++;
            }
            if (tlist.Count > 0)
            {
                schoolMap.Add(tlist);
            }

            ViewBag.StudentMap = studentMap;
            ViewBag.SchoolMap = schoolMap;
            ViewBag.VideoList = (await _studentService.GetStarStudentsAsync()) ?? new List<Instart.Models.Student>();
            ViewBag.CourseList = (await _courseService.GetRecommendListAsync(3)) ?? new List<Instart.Models.Course>();
            ViewBag.BannerList = (await _bannerService.GetBannerListByPosAsync(Instart.Models.Enums.EnumBannerPos.Student)) ?? new List<Instart.Models.Banner>();
            return View();
        }

        public async Task<ActionResult> Details(int id)
        {
            Student student = await _studentService.GetByIdAsync(id);
            IEnumerable<Student> studentList = (await _studentService.GetAllAsync()) ?? new List<Student>();
            ViewBag.StudentList = studentList;
            return View(student ?? new Student());
        }
    }
}
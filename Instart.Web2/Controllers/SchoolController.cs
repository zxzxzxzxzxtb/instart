using Instart.Common;
using Instart.Models;
using Instart.Models.Enums;
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
    /// 艺术院校
    /// </summary>
    public class SchoolController : ControllerBase
    {
        ISchoolService _schoolService = AutofacService.Resolve<ISchoolService>();
        IStudentService _studentService = AutofacService.Resolve<IStudentService>();
        IBannerService _bannerService = AutofacService.Resolve<IBannerService>();
        ICourseService _courseService = AutofacService.Resolve<ICourseService>();
        IMajorService _majorService = AutofacService.Resolve<IMajorService>();

        public SchoolController()
        {
            this.AddDisposableObject(_schoolService);
            this.AddDisposableObject(_bannerService);
            this.AddDisposableObject(_studentService);
            this.AddDisposableObject(_courseService);
            this.AddDisposableObject(_majorService);
        }

        public  ActionResult Index()
        {

            //热门搜索
            IEnumerable<School> hotList = ( _schoolService.GetHotListAsync(4)) ?? new List<School>();
            ViewBag.HotList = hotList;

            ViewBag.BannerList = ( _bannerService.GetBannerListByPosAsync(Instart.Models.Enums.EnumBannerPos.School)) ?? new List<Instart.Models.Banner>();//banner
            ViewBag.CourseList = ( _courseService.GetRecommendListAsync(3)) ?? new List<Instart.Models.Course>();//推荐课程

            //专业列表
            IEnumerable<Major> majorList = ( _majorService.GetAllAsync()) ?? new List<Major>();
            ViewBag.MajorList = majorList;

            ViewBag.CountryList = EnumberHelper.EnumToList<EnumCountry>();

            return View();
        }

        [HttpPost]
        public  JsonResult GetSchoolList(int pageIndex, int pageSize = 6, string keyword = null, int country = -1, int major = -1)
        {
            PageModel<School> schoolList =  _schoolService.GetListAsync(pageIndex, pageSize, keyword, country, major);
            IEnumerable<Student> studentList = ( _studentService.GetAllAsync()) ?? new List<Student>();

            //计算录取比例
            foreach (School school in schoolList.Data)
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
                if (schoolList.Total > 0)
                {
                    decimal rate = (decimal)count / schoolList.Total;
                    school.AcceptRate = (rate * 100).ToString("f2");
                }
            }
            
            return Success(data: new
            {
                total = schoolList.Total,
                pageSize = pageSize,
                totalPage = (int)Math.Ceiling(schoolList.Total * 1.0 / pageSize),
                list = schoolList.Data
            });
        }

        public  ActionResult Details(int id)
        {
            if (id == 0)
            {
                throw new Exception("艺术院校不存在。");
            }

            var school =  _schoolService.GetByIdAsync(id);

            if (school == null)
            {
                throw new Exception("艺术院校不存在。");
            }

            //计算录取比例
            IEnumerable<School> schoolList = ( _schoolService.GetAllAsync()) ?? new List<School>();
            IEnumerable<Student> studentList = ( _studentService.GetAllAsync()) ?? new List<Student>();
            List<Student> schoolStudents = new List<Student>();
            int count = 0;
            foreach (Student student in studentList)
            {
                if (student.SchoolId == school.Id)
                {
                    schoolStudents.Add(student);
                    count++;
                }
            }
            school.AcceptRate = "0";
            if (schoolList.Count() > 0)
            {
                decimal rate = (decimal)count / schoolList.Count();
                school.AcceptRate = (rate * 100).ToString("f2");
            }
            ViewBag.SchoolStudents = schoolStudents;
            //院校专业
            IEnumerable<SchoolMajor> schoolMajorList = ( _schoolService.GetMajorsByIdAsync(id)) ?? new List<SchoolMajor>();
            List<SchoolMajor> majorBkList = new List<SchoolMajor>(); //本科专业
            List<SchoolMajor> majorYjsList = new List<SchoolMajor>(); //研究生专业
            foreach (var item in schoolMajorList)
            {
                if (item.Type == 0)
                {
                    majorBkList.Add(item);
                }
                else if (item.Type == 1)
                {
                    majorYjsList.Add(item);
                }
            }
            ViewBag.MajorBkList = majorBkList;
            ViewBag.MajorYjsList = majorYjsList;
            //推荐课程
            ViewBag.CourseList = ( _courseService.GetRecommendListAsync(3)) ?? new List<Instart.Models.Course>();
            return View(school);
        }
    }
}
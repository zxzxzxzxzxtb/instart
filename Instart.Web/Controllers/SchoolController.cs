using Instart.Models;
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
    /// <summary>
    /// 艺术院校
    /// </summary>
    public class SchoolController : ControllerBase
    {
        ISchoolService _schoolService = AutofacService.Resolve<ISchoolService>();
        IStudentService _studentService = AutofacService.Resolve<IStudentService>();
        IBannerService _bannerService = AutofacService.Resolve<IBannerService>();
        ICourseService _courseService = AutofacService.Resolve<ICourseService>();

        public SchoolController()
        {
            this.AddDisposableObject(_schoolService);
            this.AddDisposableObject(_bannerService);
            this.AddDisposableObject(_studentService);
            this.AddDisposableObject(_courseService);
        }

        public async Task<ActionResult> Index() {

            //热门搜索
            IEnumerable<School> hotList = (await _schoolService.GetHotListAsync(4)) ?? new List<School>();
            ViewBag.HotList = hotList;

            ViewBag.BannerList = (await _bannerService.GetBannerListByPosAsync(Instart.Models.Enums.EnumBannerPos.School)) ?? new List<Instart.Models.Banner>();//banner
            ViewBag.CourseList = (await _courseService.GetRecommendListAsync(3)) ?? new List<Instart.Models.Course>();//推荐课程
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetSchoolList(int pageIndex, int pageSize = 8, string keyword = null, int country = -1, int major = -1)
        {
            PageModel<School> schoolList = await _schoolService.GetListAsync(pageIndex, pageSize, keyword, country, major);
            IEnumerable<Student> studentList = (await _studentService.GetAllAsync()) ?? new List<Student>();

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
                    decimal rate = count.ToDecimal() / schoolList.Total.ToDecimal();
                    Console.Write(rate);
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

        public async Task<ActionResult> Detail(int id)
        {
            if(id == 0)
            {
                throw new Exception("学校不存在");
            }

            var school = await _schoolService.GetByIdAsync(id);

            if(school == null)
            {
                throw new Exception("学校不存在");
            }

            return View(school);
        }
    }
}
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
    /// 艺术导师
    /// </summary>
    public class TeacherController : ControllerBase
    {
        ITeacherService _teacherService = AutofacService.Resolve<ITeacherService>();
        IDivisionService _divisionService = AutofacService.Resolve<IDivisionService>();
        IBannerService _bannerService = AutofacService.Resolve<IBannerService>();

        public TeacherController()
        {
            this.AddDisposableObject(_teacherService);
            this.AddDisposableObject(_divisionService);
            this.AddDisposableObject(_bannerService);
        }

        public async Task<ActionResult> Index(int divisionId)
        {
            var divisionList = await _divisionService.GetAllAsync();
            if (divisionList == null || divisionList.Count() == 0)
            {
                throw new Exception("请先创建学部");
            }

            if(divisionId == 0)
            {
                divisionId = divisionList.First().Id;
            }

            var teacherList = await _teacherService.GetListByDivsionAsync(divisionId, 1, 8);

            ViewBag.DivisionList = divisionList;
            ViewBag.DivisionId = divisionId;
            ViewBag.Total = teacherList.Total;
            ViewBag.TotalPages = (int)Math.Ceiling(teacherList.Total * 1.0 / 8);
            ViewBag.TeacherList = teacherList.Data ?? new List<Instart.Models.Teacher>();
            ViewBag.BannerList = (await _bannerService.GetBannerListByPosAsync(Instart.Models.Enums.EnumBannerPos.Teacher)) ?? new List<Instart.Models.Banner>();

            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetTeacherList(int divisionId, int pageIndex, int pageSize = 8)
        {
            var teacherList = await _teacherService.GetListByDivsionAsync(divisionId, pageIndex, pageSize);
            return Success(data: teacherList);
        }

        public async Task<ActionResult> Details(int id)
        {
            var teacher = await _teacherService.GetByIdAsync(id);
            if(teacher == null)
            {
                throw new Exception("导师不存在");
            }

            return View(teacher);
        }
    }
}
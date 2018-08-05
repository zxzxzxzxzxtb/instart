using Instart.Models;
using Instart.Models.Enums;
using Instart.Service;
using Instart.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Instart.Web2.Controllers
{
    /// <summary>
    /// 特色
    /// </summary>
    public class FeatureController : ControllerBase
    {
        IProgramService _programService = AutofacService.Resolve<IProgramService>();
        ICourseService _courseService = AutofacService.Resolve<ICourseService>();
        IStudioService _studioService = AutofacService.Resolve<IStudioService>();
        ICompanyService _companyService = AutofacService.Resolve<ICompanyService>();

        public FeatureController()
        {
            this.AddDisposableObject(_programService);
            this.AddDisposableObject(_courseService);
            this.AddDisposableObject(_studioService);
            this.AddDisposableObject(_companyService);
        }

        public ActionResult Index(EnumProgramType type)
        {
            var list = _programService.GetListByTypeAsync((int)type) ?? new List<Program>();
            ViewBag.Type = type.ToString();
            return View(list);
        }

        public ActionResult Show(int id)
        {
            var detail = _programService.GetByIdAsync(id);
            if (detail == null)
            {
                throw new Exception("内容不存在");
            }

            ViewBag.Type = detail.Type.ToString();
            return View(detail);
        }

        public ActionResult Division()
        {
            ViewBag.Type = "Division";
            return View();
        }

        public ActionResult Company()
        {
            ViewBag.Type = "Company";
            var list = _companyService.GetAllAsync();
            return View(list);
        }

        public ActionResult Studio()
        {
            ViewBag.Type = "Studio";
            var model = _studioService.GetInfoAsync();
            return View(model);
        }

        public ActionResult Teach()
        {
            ViewBag.Type = "Teach";
            return View();
        }

    }
}

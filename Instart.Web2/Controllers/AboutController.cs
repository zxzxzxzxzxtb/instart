using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Instart.Models;
using Instart.Service;
using Instart.Service.Base;
using Instart.Models.Enums;

namespace Instart.Web2.Controllers
{
    /// <summary>
    /// 关于Instart
    /// </summary>
    public class AboutController : ControllerBase
    {
        IAboutInstartService _aboutService = AutofacService.Resolve<IAboutInstartService>();
        IProgramService _programService = AutofacService.Resolve<IProgramService>();
        ICompanyService _companyService = AutofacService.Resolve<ICompanyService>();
        IStudioService _studioService = AutofacService.Resolve<IStudioService>();
        IDivisionService _divisionService = AutofacService.Resolve<IDivisionService>();

        public AboutController()
        {
            this.AddDisposableObject(_aboutService);
            this.AddDisposableObject(_programService);
            this.AddDisposableObject(_companyService);
            this.AddDisposableObject(_studioService);
            this.AddDisposableObject(_divisionService);
        }

        public ActionResult Index()
        {
            AboutInstart model = (_aboutService.GetInfoAsync()) ?? new AboutInstart();
            return View(model);
        }

        public ActionResult program(EnumProgramType type)
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

            ViewBag.DivisionList = _divisionService.GetAllAsync() ?? new List<Division>();

            return View();
        }

        public ActionResult Company()
        {
            ViewBag.Type = "Company";
            var list = _companyService.GetAllAsync();
            return View(list);
        }

        public ActionResult CompanyDetail(int id)
        {
            var model = _companyService.GetByIdAsync(id);
            if (model == null)
            {
                throw new Exception("内容不存在");
            }
            ViewBag.Type = "Company";
            return View(model);
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
            var model = _aboutService.GetInfoAsync() ?? new AboutInstart();
            ViewBag.PassLearning = model.PassLearning;
            return View();
        }
    }
}
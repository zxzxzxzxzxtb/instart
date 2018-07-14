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

        public SchoolController()
        {
            this.AddDisposableObject(_schoolService);
        }

        public async Task<ActionResult> Index() {
            return View();
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
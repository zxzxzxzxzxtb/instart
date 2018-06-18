using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Instart.Web.Controllers
{
    /// <summary>
    /// 艺术专业
    /// </summary>
    public class MajorController : ControllerBase
    {
        public async Task<ActionResult> Index()
        {
            await Task.Delay(1);
            return View();
        }
    }
}
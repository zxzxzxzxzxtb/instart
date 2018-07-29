using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Instart.Web2.Controllers
{
    /// <summary>
    /// 课程
    /// </summary>
    public class CourseController : ControllerBase
    {
        public  ActionResult Index() {
            return View();
        }
    }
}
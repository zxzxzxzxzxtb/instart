using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Instart.Web.Areas.Manage.Controllers
{
    public class CourseController : ManageControllerBase
    {
        public ActionResult Index() {
            return View();
        }
    }
}
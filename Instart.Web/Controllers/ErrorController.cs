using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Instart.Web.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult ErrorDetail()
        {
            return View();
        }
    }
}
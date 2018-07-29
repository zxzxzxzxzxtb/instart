using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Instart.Web2.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult ErrorDetail()
        {
            return View();
        }
    }
}
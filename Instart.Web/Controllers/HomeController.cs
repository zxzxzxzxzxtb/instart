using Instart.Service;
using Instart.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Instart.Web.Controllers
{
    public class HomeController : ControllerBase
    {
        IUserService _userService = AutofacService.Resolve<IUserService>();

        public HomeController() {
            this.AddDisposableObject(_userService);
        }

        public ActionResult Index() {
            var user = _userService.GetByName("liufei");
            return View();
        }
    }
}
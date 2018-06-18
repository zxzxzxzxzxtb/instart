using Instart.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Instart.Web.Areas.Manage.Controllers
{
    public class AccountController : ManageControllerBase
    {
        [HttpGet]
        public ActionResult Login() {
            return View();
        }

        [HttpPost]
        public ResultBase Login(string name) {
            var slt = new ResultBase();
            return slt;
        }

        [HttpPost]
        public void Quit() {
            Session["admin"] = null;
        }
    }
}
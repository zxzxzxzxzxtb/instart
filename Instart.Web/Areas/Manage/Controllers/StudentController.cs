﻿using Instart.Web.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Instart.Web.Areas.Manage.Controllers
{
    [AdminValidation]
    public class StudentController : ManageControllerBase
    {
        public ActionResult Index() {
            return View();
        }
    }
}
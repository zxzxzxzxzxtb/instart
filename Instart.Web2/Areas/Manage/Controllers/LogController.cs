using Instart.Common;
using Instart.Models.Enums;
using Instart.Service;
using Instart.Service.Base;
using Instart.Web2.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Instart.Web2.Areas.Manage.Controllers
{
    [AdminValidation]
    public class LogController : ManageControllerBase
    {
        ILogService _logService = AutofacService.Resolve<ILogService>();
        IUserService _userService = AutofacService.Resolve<IUserService>();

        public LogController()
        {
            base.AddDisposableObject(_logService);
            base.AddDisposableObject(_userService);
        }

        public ActionResult Index(int page = 1, string keyword = null, int userId = 0)
        {
            int pageSize = 10;
            var list = _logService.GetListAsync(page, pageSize, userId, keyword);
            ViewBag.Total = list.Total;
            ViewBag.PageIndex = page;
            ViewBag.TotalPages = Math.Ceiling(list.Total * 1.0 / pageSize);
            ViewBag.Keyword = keyword;
            ViewBag.UserId = userId;
            ViewBag.TypeList = EnumberHelper.EnumToList<EnumOperType>();

            var users = _userService.GetListAsync(1, 100);
            ViewBag.UserList = users.Data;

            return View(list.Data);
        }
    }
}
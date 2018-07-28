using Instart.Common;
using Instart.Models.Enums;
using Instart.Service;
using Instart.Service.Base;
using Instart.Web.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Instart.Web.Areas.Manage.Controllers
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

        public ActionResult Index(int page = 1, string keyword = null, int userId = 0, int type = -1)
        {
            int pageSize = 10;
            var list = _logService.GetListAsync(page, pageSize, keyword, userId, type);
            ViewBag.Total = list.Total;
            ViewBag.PageIndex = page;
            ViewBag.TotalPages = Math.Ceiling(list.Total * 1.0 / pageSize);
            ViewBag.Keyword = keyword;
            ViewBag.UserId = userId;
            ViewBag.Type = type;
            ViewBag.TypeList = EnumberHelper.EnumToList<EnumOperType>();

            var users = _userService.GetListAsync(1, 100);
            ViewBag.UserList = users.Data;

            return View(list.Data);
        }
    }
}
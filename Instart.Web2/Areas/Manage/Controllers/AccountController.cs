using Instart.Common;
using Instart.Service;
using Instart.Service.Base;
using Instart.Web2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Instart.Web2.Areas.Manage.Controllers
{
    public class AccountController : ManageControllerBase
    {
        IUserService _userService = AutofacService.Resolve<IUserService>();

        public AccountController()
        {
            base.AddDisposableObject(_userService);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Operation("登录系统")]
        public JsonResult Login(string username, string password, bool autoLogin = false)
        {
            if (string.IsNullOrEmpty(username))
            {
                return Error("用户名不能为空。");
            }

            if (string.IsNullOrEmpty(password))
            {
                return Error("密码不能为空。");
            }

            var user = _userService.GetByNameAsync(username.Trim());

            if (user == null)
            {
                return Error("用户不存在。");
            }

            if (Md5Helper.Encrypt(password) != user.Password.Trim())
            {
                return Error("密码错误。");
            }

            var loginUser = new LoginUser
            {
                UserId = user.Id,
                UserName = user.UserName,
                NickName = user.NickName,
                Avatar = user.Avatar,
            };

            CookieData.CurrentUser = loginUser;
            Session[WebAppSettings.SessionName] = loginUser;

            if (autoLogin)
            {
                string encryptStr = DesHelper.Encrypt(user.Id.ToString(), WebAppSettings.DesEncryptKey);
                CookieHelper.Set(WebAppSettings.CookieName, encryptStr, DateTime.Now.AddDays(3));
            }

            return Success();
        }

        [HttpPost]
        [Operation("退出系统")]
        public void Quit()
        {
            Session[WebAppSettings.SessionName] = null;
            CookieHelper.Clear(WebAppSettings.CookieName);
        }
    }
}
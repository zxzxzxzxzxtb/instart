using Instart.Common;
using Instart.Service;
using Instart.Service.Base;
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

            LogService.Write(new Instart.Models.Log
            {
                Title = $"{user.UserName}登录系统",
                UserId = user.Id,
                UserName = user.UserName,
                Type = Instart.Models.Enums.EnumOperType.Other,
            });

            return Success();
        }

        [HttpPost]
        public void Quit()
        {
            LogService.Write(new Instart.Models.Log
            {
                Title = $"{LoginUser.UserName}退出系统",
                UserId = LoginUser.UserId,
                UserName = LoginUser.UserName,
                Type = Instart.Models.Enums.EnumOperType.Other,
            });

            Session[WebAppSettings.SessionName] = null;
            CookieHelper.Clear(WebAppSettings.CookieName);
        }
    }
}
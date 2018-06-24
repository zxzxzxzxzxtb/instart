using Instart.Service;
using Instart.Service.Base;
using Instart.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Instart.Web.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class AdminValidationAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (WebAppSettings.IsDevelop)
            {
                return;
            }

            var session = filterContext.HttpContext.Session[WebAppSettings.SessionName];
            if (session == null)
            {
                var cookie = filterContext.HttpContext.Request.Cookies[WebAppSettings.CookieName];
                if (cookie == null)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "account",
                        action = "login",
                        area = "manage"
                    }));
                    return;
                }

                var userId = Common.DesHelper.Decrypt(cookie.Value, WebAppSettings.DesEncryptKey).ToInt32OrDefault(0);
                if(userId == 0)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "account",
                        action = "login",
                        area = "manage"
                    }));
                    return;
                }

                var user = AutofacService.Resolve<IUserService>().GetByIdAsync(userId).Result;
                if (user == null)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "account",
                        action = "login",
                        area = "manage"
                    }));
                    return;
                }

                var loginUser = new LoginUser
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    NickName = user.NickName,
                    Avatar = user.Avatar,
                };

                CookieData.CurrentUser = loginUser;
                filterContext.HttpContext.Session[WebAppSettings.SessionName] = loginUser;
            }
        }
    }
}
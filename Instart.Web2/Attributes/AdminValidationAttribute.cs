using Instart.Service;
using Instart.Service.Base;
using Instart.Web2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Instart.Web2.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class AdminValidationAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (WebAppSettings.IsDevelop)
            {
                var testUser = new LoginUser {
                    UserId = 10000,
                    UserName = "test",
                    NickName = "liufei",
                    Avatar = "",
                };

                CookieData.CurrentUser = testUser;
                filterContext.HttpContext.Session[WebAppSettings.SessionName] = testUser;
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

                var userId = Int32.Parse(Common.DesHelper.Decrypt(cookie.Value, WebAppSettings.DesEncryptKey));
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
                
                var user = AutofacService.Resolve<IUserService>().GetById(userId);
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
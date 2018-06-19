using Instart.Web.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Instart.Web.Attributes
{
    public class AdminValidationAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
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

                filterContext.HttpContext.Session[WebAppSettings.SessionName] = cookie.Value;
            }
        }
    }
}
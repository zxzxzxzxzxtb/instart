using Instart.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Instart.Web.Attributes
{
    /// <summary>
    /// 重复请求限制
    /// </summary>
    //[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    //public class RepeatRequestLimitAttribute : ActionFilterAttribute
    //{
    //    public RepeatRequestLimitAttribute(params string[] _RequestQuery)
    //    {
    //        this.RequestQuery = _RequestQuery;
    //        this.CacheKey = GetKey();
    //    }

    //    public string CacheKey { get; private set; }

    //    public string[] RequestQuery { get; private set; }

    //    public override void OnActionExecuting(ActionExecutingContext actionContext)
    //    {
    //        var v = CacheHelper.GetCache(CacheKey).ToInt32OrDefault(0);
    //        if (v == 1)
    //        {
    //            CacheHelper.SetCache(CacheKey, 1, DateTime.Now.AddSeconds(90));
    //            base.OnActionExecuting(actionContext);
    //            return;
    //        }

    //        var ttl = RedisHelper.Cache_TTL(CacheKey);
    //        if (ttl == -1)
    //            RedisHelper.Cache_ExpireAt(CacheKey, DateTime.Now.AddSeconds(90));

    //        var httpResponseMessage = new HttpResponseMessage();
    //        httpResponseMessage.Content = new StringContent("{\"errorcode\":-1,\"message\":\"请勿重复请求，或者重新刷新重试！\",\"success\":false}"
    //            , Encoding.UTF8, "application/json");
    //        actionContext.Response = httpResponseMessage;
    //    }

    //    public override void OnActionExecuted(ActionExecutedContext actionExecutedContext)
    //    {
    //        CacheHelper.RemoveCache(CacheKey);
    //        base.OnActionExecuted(actionExecutedContext);
    //    }
    //    string GetKey()
    //    {
    //        var rawUrl = System.Web.HttpContext.Current.Request.RawUrl.ToLower();
    //        var mdArr = new List<string> { rawUrl };
    //        if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
    //            mdArr.Add(System.Web.HttpContext.Current.User.Identity.Name.ToLower());
    //        if (RequestQuery != null && RequestQuery.Length > 0)
    //        {
    //            foreach (var Rkey in RequestQuery)
    //            {
    //                if (string.IsNullOrWhiteSpace(Rkey))
    //                {
    //                    continue;
    //                }
    //                var str = System.Web.HttpContext.Current.Request[Rkey];
    //                if (string.IsNullOrWhiteSpace(str))
    //                    continue;
    //                mdArr.Add(str.Trim().ToLower());
    //            }
    //        }
    //        var md = Md5Helper.Encrypt(string.Join("", mdArr));
    //        return string.Concat("jktweb_limit_", md);
    //    }
    //}
}
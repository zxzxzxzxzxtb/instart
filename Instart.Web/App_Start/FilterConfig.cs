using Instart.Web.Attributes;
using System.Web;
using System.Web.Mvc;

namespace Instart.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new ExceptionAttribute());
            filters.Add(new HandleErrorAttribute());            
        }
    }
}

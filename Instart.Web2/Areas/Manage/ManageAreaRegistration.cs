using System.Web.Mvc;

namespace Instart.Web2.Areas.Manage
{
    public class ManageAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "manage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "manage_default",
                "manage/{controller}/{action}/{id}",
                new {
                    action = "Index",
                    id = UrlParameter.Optional,                    
                },
                new string[] { "Instart.Web2.Areas.Manage.Controllers" }
            );
        }
    }
}
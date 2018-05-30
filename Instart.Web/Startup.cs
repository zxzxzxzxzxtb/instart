using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Instart.Web.Startup))]
namespace Instart.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

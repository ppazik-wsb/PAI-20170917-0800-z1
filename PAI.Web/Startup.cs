using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PAI.Web.Startup))]
namespace PAI.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

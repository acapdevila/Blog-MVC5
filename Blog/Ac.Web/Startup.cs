using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Ac.Web.Startup))]
namespace Ac.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

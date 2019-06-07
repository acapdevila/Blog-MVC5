using LG.Web;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Startup))]
namespace LG.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

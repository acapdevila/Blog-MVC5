using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Blog.Smoothies.Startup))]
namespace Blog.Smoothies
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

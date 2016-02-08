using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Seo.Route.Startup))]
namespace Seo.Route
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

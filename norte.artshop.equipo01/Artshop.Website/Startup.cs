using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Artshop.Website.Startup))]
namespace Artshop.Website
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

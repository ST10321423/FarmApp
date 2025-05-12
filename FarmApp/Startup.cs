using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FarmApp.Startup))]
namespace FarmApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

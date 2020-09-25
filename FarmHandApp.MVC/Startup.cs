using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FarmHandApp.MVC.Startup))]
namespace FarmHandApp.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

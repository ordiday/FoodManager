using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FoodManager.Web.Startup))]
namespace FoodManager.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

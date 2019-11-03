using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SmartKitchen3.Startup))]
namespace SmartKitchen3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

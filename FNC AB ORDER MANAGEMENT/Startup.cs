using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FNC_AB_ORDER_MANAGEMENT.Startup))]
namespace FNC_AB_ORDER_MANAGEMENT
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

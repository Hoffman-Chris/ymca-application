using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ymca_application.Startup))]
namespace ymca_application
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

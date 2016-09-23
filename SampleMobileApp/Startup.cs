using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SampleMobileApp.Startup))]

namespace SampleMobileApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}
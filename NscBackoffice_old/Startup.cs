using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NscBackoffice.Startup))]
namespace NscBackoffice
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

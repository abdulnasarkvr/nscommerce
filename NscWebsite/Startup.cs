using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NscWebsite.Startup))]
namespace NscWebsite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NscSeller.Startup))]
namespace NscSeller
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

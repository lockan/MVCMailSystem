using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCMailSystem.Startup))]
namespace MVCMailSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

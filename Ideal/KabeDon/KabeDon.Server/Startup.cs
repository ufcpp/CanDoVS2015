using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KabeDon.Server.Startup))]
namespace KabeDon.Server
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

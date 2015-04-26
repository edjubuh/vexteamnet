using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VexTeamNetwork.Startup))]
namespace VexTeamNetwork
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

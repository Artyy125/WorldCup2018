using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WorldCup2018.Startup))]
namespace WorldCup2018
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PrograWebProjectEJ.Startup))]
namespace PrograWebProjectEJ
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

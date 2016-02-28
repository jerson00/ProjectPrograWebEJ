using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjectEJ.Startup))]
namespace ProjectEJ
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KNC.Startup))]
namespace KNC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

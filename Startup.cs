using Hangfire;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(KNC.Startup))]
namespace KNC
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();

            GlobalConfiguration.Configuration
                .UseSqlServerStorage("DefaultConnection"); // Use your connection string name

            app.UseHangfireDashboard("/hangfire");
            app.UseHangfireServer();
        }
    }
}

using Hangfire;
using Owin;
using Microsoft.Owin;

namespace TSHF.Hangfire
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseHangfireDashboard();
            //app.UseHangfireDashboard( "/hangfire", new DashboardOptions
            //{
            //    Authorization = new[] { new LocalNetworkAuthorizationFilter() },
            //} );
        }
    }
}
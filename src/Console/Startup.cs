using Microsoft.Owin.Cors;
using Owin;

namespace Adform.SummerCamp.TowerDefense.Console
{
    class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
        }
    }
}
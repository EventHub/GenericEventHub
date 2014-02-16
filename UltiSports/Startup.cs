using Microsoft.Owin;
using Owin;
using UltiSports;

[assembly: OwinStartup(typeof(Startup))]
namespace UltiSports
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
using Microsoft.Owin;
using Ninject;
using Owin;
using UltiSports;
using UltiSports.ApiControllers;
using UltiSports.App_Start;

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
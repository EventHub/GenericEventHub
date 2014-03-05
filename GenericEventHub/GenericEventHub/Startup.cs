using Microsoft.Owin;
using Owin;
using GenericEventHub;

namespace GenericEventHub
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
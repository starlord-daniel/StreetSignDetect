using Microsoft.Owin;
using Owin;
using SignalRService.Objects;

[assembly: OwinStartup(typeof(SignalRService.Startup))]
namespace SignalRService
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            IndexCounter.index = 0;

            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();
        }
    }
}

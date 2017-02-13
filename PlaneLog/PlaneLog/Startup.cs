using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PlaneLog.Startup))]
namespace PlaneLog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

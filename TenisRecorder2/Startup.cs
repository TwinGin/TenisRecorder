using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TenisRecorder2.Startup))]
namespace TenisRecorder2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StreamingMvc.Startup))]
namespace StreamingMvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

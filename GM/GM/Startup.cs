using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GM.Startup))]
namespace GM
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

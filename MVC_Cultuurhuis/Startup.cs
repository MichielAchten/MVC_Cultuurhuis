using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC_Cultuurhuis.Startup))]
namespace MVC_Cultuurhuis
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

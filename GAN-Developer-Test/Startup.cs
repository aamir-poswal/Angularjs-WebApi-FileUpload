using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GAN_Developer_Test.Startup))]
namespace GAN_Developer_Test
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

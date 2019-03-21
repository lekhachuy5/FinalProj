using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestSP.Startup))]
namespace TestSP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

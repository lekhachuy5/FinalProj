using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LayOut.Startup))]
namespace LayOut
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyEcommerce.Startup))]
namespace MyEcommerce
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

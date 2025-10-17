using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyNerdDinner.Startup))]
namespace MyNerdDinner
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

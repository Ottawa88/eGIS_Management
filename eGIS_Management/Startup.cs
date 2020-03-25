using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(eGIS_Management.Startup))]
namespace eGIS_Management
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

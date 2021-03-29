using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BankTr.Startup))]
namespace BankTr
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

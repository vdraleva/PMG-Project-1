using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(PMG.App.Areas.Identity.IdentityHostingStartup))]
namespace PMG.App.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}
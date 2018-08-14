using System;
using DemoMySQLEF.Areas.Identity.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(DemoMySQLEF.Areas.Identity.IdentityHostingStartup))]
namespace DemoMySQLEF.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<IdentityContext>(options =>
                    options.UseMySql(  
                        context.Configuration.GetConnectionString("IdentityContextConnection")));

                services.AddDefaultIdentity<DemoUser>()
                    .AddEntityFrameworkStores<IdentityContext>();
            });
        }
    }
}
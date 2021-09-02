using System;
using Kursmoment3.Areas.Identity.Data;
using Kursmoment3.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Kursmoment3.Areas.Identity.IdentityHostingStartup))]
namespace Kursmoment3.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<Kursmoment3DbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("Kursmoment3DbContextConnection")));

                services.AddDefaultIdentity<Kursmoment3User>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                })
                    .AddEntityFrameworkStores<Kursmoment3DbContext>();
            
            });
        }
    }
}
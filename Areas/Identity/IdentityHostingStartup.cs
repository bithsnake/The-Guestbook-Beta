using System;
using TheGuestBook.Areas.Identity.Data;
using TheGuestBook.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(TheGuestBook.Areas.Identity.IdentityHostingStartup))]
namespace TheGuestBook.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<TheGuestBookDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("TheGuestBookDbContextConnection")));

                services.AddDefaultIdentity<TheGuestBookUser>(options =>
                {
                    /* Här ställer man in om mail bekräftelse och och lösneords kriterier*/
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                })
                    .AddEntityFrameworkStores<TheGuestBookDbContext>();
            
            });
        }
    }
}
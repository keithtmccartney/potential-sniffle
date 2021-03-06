using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using potential_sniffle.Application.Common.Interfaces;
using potential_sniffle.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace potential_sniffle.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            //{
            //    services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("potential-sniffle"));
            //}
            //else
            //{
            //    services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            //}

            //services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

            //services.AddScoped<IDomainEventService, DomainEventService>();

            //services.AddDefaultIdentity<ApplicationUser>().AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            //services.AddIdentityServer().AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

            //services.AddTransient<IDateTime, DateTimeService>();

            //services.AddTransient<IIdentityService, IdentityService>();

            //services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();

            //services.AddAuthentication().AddIdentityServerJwt();

            //services.AddAuthorization(options => options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator")));

            return services;
        }
    }
}

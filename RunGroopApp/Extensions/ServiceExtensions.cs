using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RunGroopApp.Data;
using RunGroopApp.Models;

namespace RunGroopApp.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) =>
          services.AddCors(options =>
          {
              options.AddPolicy("CorsPolicy", builder =>
              builder.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
          });


        public static void ConfigureIISIntegration(this IServiceCollection services) =>
          services.Configure<IISOptions>(options =>
          {

          });


        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var Connection = configuration.GetSection("ConnectionString")["DefaultConn"];

            services.AddDbContext<ApplicationDbContext>(options =>
            {

                options.UseSqlServer(Connection);
            });

            /* services.AddIdentity<IdentityUser, IdentityRole>(options =>
             {
                 options.SignIn.RequireConfirmedAccount = false;
             })
             .AddEntityFrameworkStores<ApplicationDbContext>();*/


            services.AddIdentity<AppUser, IdentityRole>()
           .AddEntityFrameworkStores<ApplicationDbContext>();

        }

    }
}

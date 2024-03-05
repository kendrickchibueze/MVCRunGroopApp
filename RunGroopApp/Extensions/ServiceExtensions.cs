using Microsoft.EntityFrameworkCore;
using RunGroopApp.Data;
using RunGroopApp.Implementations;
using RunGroopApp.Interfaces;
using RunGroopApp.Services;
using RunGroopApp.Services.LoggerService;

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

        public static void ConfigureLoggerService(this IServiceCollection services) =>
        services.AddSingleton<ILoggerManager, LoggerManager>();

        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var Connection = configuration.GetSection("ConnectionString")["DefaultConn"];

            services.AddDbContext<ApplicationDbContext>(options =>
            {

                options.UseSqlServer(Connection);
            });

            /* services.AddIdentity<AppUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();*/

            services.AddScoped<IUnitOfWork, UnitOfWork<ApplicationDbContext>>();
            services.AddScoped<IClubService, ClubService>();
            services.AddScoped<IRaceService, RaceService>();

        }

    }
}

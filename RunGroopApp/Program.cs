using RunGroopApp.Data;
using RunGroopApp.Extensions;

namespace RunGroopApp
{
    public class Program
    {
        public static async void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.ConfigureCors();
            builder.Services.ConfigureIISIntegration();
            builder.Services.ConfigureServices(builder.Configuration);
            builder.Services.AddControllersWithViews();

            var app = builder.Build();



            if (args.Length == 1 && args[0].ToLower() == "seeddata")
            {
                  await  Seed.SeedUsersAndRolesAsync(app);
                  Seed.SeedData(app);
            }






            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
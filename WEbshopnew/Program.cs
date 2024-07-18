using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using WEbshopnew.DataAccess.Data;
using WEbshopnew.DataAccess.Repository;
using WEbshopnew.DataAccess.Repository.IRepository;


namespace webshoping
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            // Register DbContext with connection string
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString)); // Ensure "DataAccess.Data.ApplicationDbContext" is correct

           // builder.Services.AddDatabaseDeveloperPageExceptionFilter();
           builder.Services.AddScoped<ICatagoryRepository, CatagoryRepository>();   
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
              //  app.UseMigrationsEndPoint(); // Provides a UI for applying migrations in development
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts(); // Use HSTS (HTTP Strict Transport Security) in production
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication(); // Use authentication
            app.UseAuthorization(); // Use authorization

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages(); // Map Razor Pages

            app.Run(); // Start the application
        }

    }
}

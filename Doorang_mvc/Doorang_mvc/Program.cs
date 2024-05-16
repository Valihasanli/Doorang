using Doorang_mvc.DAL;
using Doorang_mvc.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Doorang_mvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<AppDbContext>(opt =>
                        {
                            opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
                        }
                        );
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddIdentity<User,IdentityRole>(opt =>
            {
                opt.Password.RequiredLength = 6;
                opt.User.RequireUniqueEmail = true;
                opt.User.AllowedUserNameCharacters = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890.,_-";
                opt.Lockout.MaxFailedAccessAttempts = 5;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(180);
            }).AddEntityFrameworkStores<AppDbContext>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
          );

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();

        }
    }
}

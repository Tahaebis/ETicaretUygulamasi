using ETicaretUygulamasi.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace ETicaretUygulamasi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Add Session service
            builder.Services.AddSession(opts =>
            {
                opts.Cookie.Name = "eticaretuyg.session";
                opts.IdleTimeout = TimeSpan.FromMinutes(20);
            });

            // DatabaseContext nesnemizin merkezi tanýmlanmasý.
            builder.Services.AddDbContext<DatabaseContext>(opts =>
            {
                string connStr = builder.Configuration.GetConnectionString("DefaultConnection");
                opts.UseSqlServer(connStr);
                opts.UseLazyLoadingProxies();
            });

            // Authentication - kimlik kontrolü hizmetini aktif ettik.
            builder.Services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(opts =>
                {
                    opts.Cookie.Name = "eticaretuyg.auth";
                    opts.LoginPath = "/Auth/Login";
                    opts.LogoutPath = "/Auth/Logout";
                    opts.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                    opts.SlidingExpiration = false;
                });
                
            
            
            
            
            var app = builder.Build();








            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}

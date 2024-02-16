using BilgeShop.Business.Managers;
using BilgeShop.Business.Services;
using BilgeShop.Data.Context;
using BilgeShop.Data.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BilgeShop.WebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            var connectionString = builder.Configuration.GetConnectionString("HomeConnection");

            builder.Services.AddDbContext<BilgeShopContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

			builder.Services.AddScoped<IUserService, UserManager>();
			builder.Services.AddScoped<ICategoryService, CategoryManager>();
            builder.Services.AddScoped<IProductService, ProductManager>();      

			builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {

                options.LoginPath = new PathString("/");
				options.LogoutPath = new PathString("/");
                options.AccessDeniedPath = new PathString("/ Errors / Error403");

				// giri�- ��k�� - eri�im engeli durumlar�nda y�nlendirilecek olan adresler.

			});

            // TODO  : AccesDenied sorunu ��z�lecek , 403 i�in

            var contentRootPath = builder.Environment.ContentRootPath;

            var keysDirectory = new DirectoryInfo(Path.Combine(contentRootPath, "App_Data", "Keys"));

            builder.Services.AddDataProtection()
            .SetApplicationName("BilgeShop")
            .SetDefaultKeyLifetime(new TimeSpan(9999, 0, 0, 0))
            .PersistKeysToFileSystem(keysDirectory);

            // App_Data -> Keys -> i�erisindeki xml dostyas�na sahip her proje ayn� �ifreleme/�ifre a�ma y�ntemi kullanaca��ndan, birbirlerinin �ifrelkerini a�abilir 

			var app = builder.Build();

            app.UseStaticFiles(); // www.root i�in 

            app.UseAuthentication();
            app.UseAuthorization();
            // Auth i�lemleri yap�yorsan, �stteki 2 sat�r yaz�lmal�. yoksa hata vermez fakat oturum a�maz, yetkilendirme sorgulayamaz 

            app.UseStatusCodePagesWithRedirects("/Errors/Error{0}");

            // Area i�in yaz�lan root her zaman default un �zerinde olmasl� yoksa default a istek att�n zanneder

            app.MapControllerRoute(
              name: "areas",
              pattern: "{area:exists}/{Controller=Dashboard}/{Action=Index}/{id?}"
              );

            app.MapControllerRoute(
                name:"Default",
                pattern:"{Controller=Home}/{Action=Index}/{id?}"
                );

            app.Run();
        }
    }
}

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

				// giriþ- çýkýþ - eriþim engeli durumlarýnda yönlendirilecek olan adresler.

			});

            // TODO  : AccesDenied sorunu çözülecek , 403 için

            var contentRootPath = builder.Environment.ContentRootPath;

            var keysDirectory = new DirectoryInfo(Path.Combine(contentRootPath, "App_Data", "Keys"));

            builder.Services.AddDataProtection()
            .SetApplicationName("BilgeShop")
            .SetDefaultKeyLifetime(new TimeSpan(9999, 0, 0, 0))
            .PersistKeysToFileSystem(keysDirectory);

            // App_Data -> Keys -> içerisindeki xml dostyasýna sahip her proje ayný þifreleme/þifre açma yöntemi kullanacaðýndan, birbirlerinin þifrelkerini açabilir 

			var app = builder.Build();

            app.UseStaticFiles(); // www.root için 

            app.UseAuthentication();
            app.UseAuthorization();
            // Auth iþlemleri yapýyorsan, üstteki 2 satýr yazýlmalý. yoksa hata vermez fakat oturum açmaz, yetkilendirme sorgulayamaz 

            app.UseStatusCodePagesWithRedirects("/Errors/Error{0}");

            // Area için yazýlan root her zaman default un üzerinde olmaslý yoksa default a istek attýn zanneder

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

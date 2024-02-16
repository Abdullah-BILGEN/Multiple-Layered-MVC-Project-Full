using BilgeShop.Business.Dtos;
using BilgeShop.Business.Services;
using BilgeShop.WebUI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace BilgeShop.WebUI.Controllers
{
	// Authentication and Authorization
	// Kimlik Doğrulama - Yetkilendirme 
	public class AuthController : Controller
	{

		private readonly IUserService _userService;

		public AuthController(IUserService userService)
		{
			_userService = userService;
		}


		[HttpGet]
		[Route("KayitOl")]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[Route("KayitOl")]

		public IActionResult Register(RegisterViewModel formData)
		{
			if (!ModelState.IsValid)
			{
				return View(formData);
			}

			var userAddDto = new UserAddDto()
			{
				FirstName = formData.FirstName.Trim(),
				LastName = formData.LastName.Trim(),
				Email = formData.EMail.Trim(),
				Password = formData.Password.Trim(),
			};

			var result = _userService.AddUser(userAddDto);

			if (result.IsSucceed)
			{
				TempData["SuccessMessage"] = result.Message;
				return RedirectToAction("Index", "Home");
				//temp data redirectoaction her yerde çağrılabilir 

			}
			else
			{
				ViewBag.ErrorMessage = result.Message;
				return View(formData);

			}


			return RedirectToAction("Index", "Home");
		}


		public async Task<IActionResult> Login(LoginViewModel formData)
		{

			var loginDto = new UserLogindto()
			{
				Email = formData.Email,
				Password = formData.Password
			};

			var userInfo = _userService.LoginUser(loginDto);
			if (userInfo is null)
			{
				// Uyarı mesajı verebilir 
				return RedirectToAction("Index", "Home");
			}
			// Burqaya kadar geldiyse demek ki oturum açabilirim.

			var claims = new List<Claim>();

			claims.Add(new Claim("id", userInfo.Id.ToString()));
			claims.Add(new Claim("email", userInfo.Email.ToString()));
			claims.Add(new Claim("firstName", userInfo.FirstName.ToString()));
			claims.Add(new Claim("lastName", userInfo.LastName.ToString()));
			claims.Add(new Claim("userType", userInfo.UserType.ToString()));

			// YETKILRNDIRME (AUTHORIZATION ICIN) GEREKLİ OLAN ALTTAKI KOD !!
			claims.Add(new Claim(ClaimTypes.Role, userInfo.UserType.ToString())); // ClaimTypes.Role -> .net içerisinde authorization mekanizması ile paralel çalışacak.

			var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);


			var authProperties = new AuthenticationProperties
			{
				AllowRefresh = true,
				ExpiresUtc = new DateTimeOffset(DateTime.Now.AddHours(48)) // oturum 48 saat açık kalsın kodu
			};
			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity), authProperties);

			TempData["SuccessMessage"] = "Kullanıcı girişi başırı ile gerçekleştirildi ";

			return RedirectToAction("Index", "Home");
		}

		public async Task<IActionResult> Logout()
		{

			await HttpContext.SignOutAsync();
			TempData["SuccessMessage"] = "Oturumdan Başarıyla Çıkış Yapıldı.";


			return RedirectToAction("Index", "Home");
		}
	}
}

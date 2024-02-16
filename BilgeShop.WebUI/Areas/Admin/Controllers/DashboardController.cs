using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BilgeShop.WebUI.Areas.Admin.Controllers
{
	[Area("Admin")] // Program cs deki area:exsits ile eşleşir.
	[Authorize(Roles ="Admin")] // Claimlerdeki claimTypes.Role ile paralel çalışır. Admin olmayanlar bu kontrole e istek atamaz 
	public class DashboardController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}

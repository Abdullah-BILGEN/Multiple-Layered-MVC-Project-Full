using Microsoft.AspNetCore.Mvc;

namespace BilgeShop.WebUI.Controllers
{
    public class HomeController : Controller
    {

        [Route("/")]
        [Route("urunler/{categoryName}/{catewgoryId}")]
        public IActionResult Index(int? categoryId = null)
        {

             ViewBag.CategoryId = categoryId;   

            return View();
        }
    }
}

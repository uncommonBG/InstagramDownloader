using Microsoft.AspNetCore.Mvc;

namespace InstagramDownloader.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

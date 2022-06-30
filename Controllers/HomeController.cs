using Microsoft.AspNetCore.Mvc;

namespace EduhomePraktika._1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

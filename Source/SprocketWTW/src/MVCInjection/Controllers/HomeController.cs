using Microsoft.AspNetCore.Mvc;
using MVCInjection.Injectables;

namespace MVCInjection.Controllers
{
    public class HomeController : Controller
    {

        public HomeController(ITitleProvider titleProvider)
        {
            ViewData["Title"] = titleProvider.Title;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace WebApplication
{
    public class Startup : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

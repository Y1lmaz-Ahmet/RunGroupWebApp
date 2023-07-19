using Microsoft.AspNetCore.Mvc;

namespace RunGroupWebApp.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

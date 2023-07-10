using Microsoft.AspNetCore.Mvc;

namespace RunGroupWebApp.Controllers
{
	public class ClubController : Controller
	{
		//Index in the Controller => Index.cshtml in the View folder > Club > Index.cshtml
		//it has to start with "Index" in the club folder if the IActionResult is set to Index()
		public IActionResult Index()
		{
			return View();
		}
	}
}

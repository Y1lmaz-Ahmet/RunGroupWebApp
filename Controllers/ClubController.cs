using Microsoft.AspNetCore.Mvc;
using RunGroupWebApp.Data;

namespace RunGroupWebApp.Controllers
{
	public class ClubController : Controller
	{
        private readonly ApplicationDBContext _context;

        //ApplicationDBContext = database and brings back the whole table
        public ClubController(ApplicationDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()//"C" => MVC
		{
            var clubs = _context.Clubs.ToList();//"M" => MVC
			return View(clubs);//"V" => MVC
		}
	}
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroupWebApp.Data;
using RunGroupWebApp.Models;

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
        public IActionResult Detail(int id)
        {
            Club club = _context.Clubs.Include(a => a.Address).FirstOrDefault(x => x.Id == id);
            return View(club);
        }
	}
}

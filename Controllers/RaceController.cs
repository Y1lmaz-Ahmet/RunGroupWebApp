using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroupWebApp.Data;
using RunGroupWebApp.Models;

namespace RunGroupWebApp.Controllers
{
	public class RaceController : Controller
	{
		private readonly ApplicationDBContext _context;
        public RaceController(ApplicationDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
		{
			var races = _context.Races.ToList();
			return View(races);
		}

		public IActionResult Detail(int id)
		{
			Race race = _context.Races.Include(i => i.Address).SingleOrDefault(r => r.Id == id);
			return View(race);
		}
	}
}

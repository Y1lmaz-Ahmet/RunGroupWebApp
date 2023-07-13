using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroupWebApp.Data;
using RunGroupWebApp.Interfaces;
using RunGroupWebApp.Models;
using RunGroupWebApp.Repository;

namespace RunGroupWebApp.Controllers
{
	public class RaceController : Controller
	{
        private readonly IRaceRepository _raceRepository;

        public RaceController(IRaceRepository raceRepository)
        {
            _raceRepository = raceRepository;
        }
        public async Task<IActionResult> Index()
		{
			IEnumerable<Race> races = await _raceRepository.GetAll();
			return View(races);
		}

		public async Task<IActionResult> Detail(int id)
		{
			Race race = await _raceRepository.GetByIdAsync(id);
			return View(race);
		}

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Race race)
        {
            if (!ModelState.IsValid)
            {
                return View(race);
            }
            _raceRepository.Add(race);
            return RedirectToAction("Index");
        }
    }
}

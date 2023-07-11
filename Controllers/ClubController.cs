using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroupWebApp.Data;
using RunGroupWebApp.Interfaces;
using RunGroupWebApp.Models;

namespace RunGroupWebApp.Controllers
{
	public class ClubController : Controller
	{
        
        private readonly IClubRepository _clubRepository;

        //ApplicationDBContext = database and brings back the whole table
        public ClubController(IClubRepository clubRepository)
        {
            _clubRepository = clubRepository;
        }
        public async Task<IActionResult> Index()//"C" => MVC
		{
            IEnumerable<Club> clubs = await _clubRepository.GetAll();//"M" => MVC
			return View(clubs);//"V" => MVC
		}
        public async Task<IActionResult> Detail(int id)
        {
            Club club = await _clubRepository.GetByIdAsync(id);
            return View(club);
        }
	}
}

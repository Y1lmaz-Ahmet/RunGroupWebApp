using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroupWebApp.Data;
using RunGroupWebApp.Interfaces;
using RunGroupWebApp.Models;
using RunGroupWebApp.Repository;
using RunGroupWebApp.Services;
using RunGroupWebApp.ViewModels;

namespace RunGroupWebApp.Controllers
{
	public class RaceController : Controller
	{
        private readonly IRaceRepository _raceRepository;
        private readonly IPhotoService _photoService;

        public RaceController(IRaceRepository raceRepository,IPhotoService photoService)
        {
            _raceRepository = raceRepository;
            _photoService = photoService;
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
        public async Task<IActionResult> Create(CreateRaceViewModel raceVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(raceVM.Image);

                var race = new Race
                {
                    Title = raceVM.Title,
                    Description = raceVM.Description,
                    Image = result.Url.ToString(),
                    Address = new Address
                    {
                        Street = raceVM.Address.Street,
                        City = raceVM.Address.City,
                        State = raceVM.Address.State,
                    }
                };
                _raceRepository.Add(race);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo Upload FAILED!");
            }
            return View(raceVM);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var race = await _raceRepository.GetByIdAsync(id);
            if (race == null) return View("No race found");
            var RaceVM = new EditRaceViewModel
            {
                Title = race.Title,
                Description = race.Description,
                AddressId = race.AddressId,
                Address = race.Address,
                URL = race.Image,
                RaceCategory = race.RaceCategory
            };
            return View(RaceVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditRaceViewModel raceVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View("Edit", raceVM);
            }
            var userRacer = await _raceRepository.GetByIdAsyncNoTracking(id);

            if (userRacer != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(userRacer.Image);

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete the photo");
                    return View(raceVM);
                }


                var photoResult = await _photoService.AddPhotoAsync(raceVM.Image);
                var race = new Race
                {
                    Id = id,
                    Title = raceVM.Title,
                    Description = raceVM.Description,
                    Image = photoResult.Url.ToString(),
                    AddressId = raceVM.AddressId,
                    Address = raceVM.Address,

                };

                _raceRepository.Update(race);
                return RedirectToAction("Index");
            }
            else
            {
                return View(raceVM);
            }
        }
    }
}

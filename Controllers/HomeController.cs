using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RunGroupWebApp.Helpers;
using RunGroupWebApp.Interfaces;
using RunGroupWebApp.Models;
using RunGroupWebApp.ViewModels;
using System.Diagnostics;
using System.Globalization;
using System.Net;

namespace RunGroupWebApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IClubRepository _clubRepository;

		public HomeController(ILogger<HomeController> logger,IClubRepository clubRepository)
		{
			_logger = logger;
			_clubRepository = clubRepository;
		}

		public async Task<IActionResult> Index()
		{
			var ipInfo = new IPinfo();
			var homeViewModel = new HomeViewModel();
			try
			{
				
				string url = "https://ipinfo.io/212.233.45.14?token=9b38b4d5a60fde";
				var info = new WebClient().DownloadString(url);
				//takes the JSON and turns it into an Object = DeserializeObject
				ipInfo = JsonConvert.DeserializeObject<IPinfo>(info);
				RegionInfo myRII = new RegionInfo(ipInfo.Country);
				ipInfo.Country = myRII.EnglishName;
				homeViewModel.City = ipInfo.City;
				homeViewModel.State = ipInfo.Region;
				if(homeViewModel.City != null)
				{
					homeViewModel.Clubs = await _clubRepository.GetClubByCity(homeViewModel.City);
				}
                return View(homeViewModel);

                
			}
			catch (Exception ex)
			{
				homeViewModel.Clubs = null;
			}
			return View(homeViewModel);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
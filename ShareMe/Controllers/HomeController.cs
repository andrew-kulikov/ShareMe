using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShareMe.Core;
using ShareMe.Core.Models;
using ShareMe.ViewModels;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ShareMe.Services;

namespace ShareMe.Controllers
{
	public class HomeController : Controller
	{
		private readonly IPhotoService _photoService;
		private readonly IUserService _userService;

		public HomeController(IPhotoService photoService, IUserService userService)
		{
			_photoService = photoService;
			_userService = userService;
		}

		[Authorize]
		public async Task<IActionResult> Index()
		{
			var userName = (await _userService.GetUserAsync(HttpContext.User)).UserName;
			var photos = _photoService.GetUserPhotos(userName);

			return View("Photos", photos);
		}

		public IActionResult About()
		{
			ViewData["Message"] = "Your application description page.";

			return View();
		}

		public IActionResult Contact()
		{
			ViewData["Message"] = "Your contact page.";

			return View();
		}

		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}

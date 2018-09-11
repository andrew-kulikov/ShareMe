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
using ShareMe.ViewModels.PhotoViewModels;

namespace ShareMe.Controllers
{
	public class HomeController : Controller
	{
		private readonly IPhotoService _photoService;
		private readonly IUserService _userService;
		private readonly IFollowingService _followingService;

		public HomeController(IPhotoService photoService, 
			IUserService userService, IFollowingService followingService)
		{
			_photoService = photoService;
			_userService = userService;
			_followingService = followingService;
		}

		[Authorize]
		public async Task<IActionResult> Index()
		{
			var user = await _userService.GetUserAsync(HttpContext.User);
			var userName = user.UserName;
			var photos = _photoService.GetUserPhotos(userName);
			var followings = _followingService.GetUserFollowings(user.Id)
				.ToLookup(f => f.FolloweeId);

			var viewModel = new PhotosViewModel
			{
				Photos = photos.ToList(),
				Followings = followings
			};

			return View("Photos", viewModel);
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

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using ShareMe.Services.Interfaces;
using ShareMe.ViewModels;
using ShareMe.ViewModels.PhotoViewModels;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

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
		public async Task<IActionResult> Index([FromQuery]string search="")
		{
			var user = await _userService.GetUserAsync(HttpContext.User);
			var userName = user.UserName;
			var photos = _photoService.GetAllPhotos()
				.ToList()
				.Where(p => p.Description.Contains(search) ||
				            p.Tags.Any(t => t.Tag.Name.Contains(search)));
			var followings = _followingService.GetUserFollowings(user.Id)
				.ToLookup(f => f.FolloweeId);

			var viewModel = new PhotosViewModel
			{
				Photos = photos.ToList(),
				Followings = followings
			};

			return View("Photos", viewModel);
		}

		[HttpPost]
		public IActionResult SetLanguage(string culture, string returnUrl)
		{
			Response.Cookies.Append(
				CookieRequestCultureProvider.DefaultCookieName,
				CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
				new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
			);

			return LocalRedirect(returnUrl);
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

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShareMe.Core.Models;
using ShareMe.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace ShareMe.Controllers
{
	[Authorize(Roles = "Admin")]
	public class AdminController : Controller
	{
		private readonly IPhotoService _photoService;
		private readonly IUserService _userService;
		private readonly IFollowingService _followingService;
		private readonly IRatingService _ratingService;
		private readonly UserManager<AspNetUsers> _userManager;

		public AdminController(IPhotoService photoService, IRatingService ratingService,
			IUserService userService, IFollowingService followingService, UserManager<AspNetUsers> userManager)
		{
			_photoService = photoService;
			_userService = userService;
			_followingService = followingService;
			_ratingService = ratingService;
			_userManager = userManager;
		}

		[Route("/Account/Admin")]
		public async Task<IActionResult> Index()
		{
			var adminName = User.Identity.Name;
			var allAsers = await _userService.GetAll();
			var users = allAsers
				.Where(u => u.UserName != adminName)
				.ToList();

			return View(users);
		}

		[HttpPost]
		public async Task<ActionResult> DeleteUser(string userName)
		{
			if (userName == User.Identity.Name)
				return BadRequest();

			var user = await _userService.GetUserByName(userName);
			await _userManager.DeleteAsync(user);

			return RedirectToAction("Index");
		}
	}
}
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShareMe.Core;
using ShareMe.Core.Models;
using ShareMe.Services;
using ShareMe.ViewModels.PhotoViewModels;

namespace ShareMe.Controllers
{
	public class PhotosController : Controller
	{
		private readonly IPhotoService _photoService;
		private readonly IUserService _userService;

		public PhotosController(IPhotoService photoService, IUserService userService)
		{
			_photoService = photoService;
			_userService = userService;
		}

		[Authorize]
		public IActionResult Create()
		{
			var viewModel = new PhotoFormViewModel();
			return View(viewModel);
		}

		[Authorize]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(PhotoFormViewModel viewModel)
		{
			if (!ModelState.IsValid)
				return View(viewModel);

			var user = await _userService.GetUserAsync(HttpContext.User);

			var photo = new Photo
			{
				Url = viewModel.Url,
				Description = viewModel.Description,
				UserId = user.Id
			};

			_photoService.AddPhoto(photo);

			return RedirectToAction("Index", "Home");
		}
	}
}
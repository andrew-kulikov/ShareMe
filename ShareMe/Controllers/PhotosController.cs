﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShareMe.Core.Models;
using ShareMe.Services;
using ShareMe.ViewModels.PhotoViewModels;
using System.Threading.Tasks;

namespace ShareMe.Controllers
{
	public class PhotosController : Controller
	{
		private readonly IPhotoService _photoService;
		private readonly IUserService _userService;
		private readonly IFollowingService _followingService;
		private readonly IRatingService _ratingService;

		public PhotosController(IPhotoService photoService, IRatingService ratingService,
			IUserService userService, IFollowingService followingService)
		{
			_photoService = photoService;
			_userService = userService;
			_followingService = followingService;
			_ratingService = ratingService;
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

		[Route("id:int")]
		public async Task<ActionResult> Details(int id)
		{
			var photo = _photoService.GetPhoto(id);
			var user = await _userService.GetUserAsync(HttpContext.User);
			var following = _followingService.IsFollowing(user.Id, photo.UserId);
			var liked = _ratingService.RatingExists(user.Id, id, "Like");

			var viewModel = new PhotoDetailsViewModel
			{
				Photo = photo,
				Following = following,
				Liked = liked
			};

			return View(viewModel);
		}
	}
}
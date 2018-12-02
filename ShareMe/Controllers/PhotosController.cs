using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShareMe.Core.Models;
using ShareMe.Services.Interfaces;
using ShareMe.ViewModels.PhotoViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ShareMe.Controllers
{
	[Authorize]
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

		public IActionResult Create()
		{
			var viewModel = new PhotoFormViewModel();
			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(PhotoFormViewModel viewModel)
		{
			if (!ModelState.IsValid)
				return View(viewModel);

			var user = await _userService.GetUserAsync(HttpContext.User);
			var tags = _photoService.ParseTags(viewModel.Tags);

			var photo = new Photo
			{
				Url = viewModel.Url,
				Description = viewModel.Description,
				UserId = user.Id,
				Created = DateTime.Now,
			};

			_photoService.AddPhoto(photo);


			await _photoService.BindTagsAsync(photo, tags);

			return RedirectToAction("Index", "Home");
		}

		[Route("{id:int}")]
		public async Task<ActionResult> Details(int id)
		{
			var photo = _photoService.GetPhoto(id);
			var user = await _userService.GetUserAsync(HttpContext.User);
			var following = _followingService.IsFollowing(user.Id, photo.UserId);
			var liked = _ratingService.RatingExists(user.Id, id, "Like");
			var likesCount = photo.Ratings.Count(r => r.Type.Name == "Like");

			var viewModel = new PhotoDetailsViewModel
			{
				Photo = photo,
				Following = following,
				Liked = liked,
				LikesCount = likesCount,
				IsCreator = user.Id == photo.UserId,
				UserId = user.Id
			};

			return View(viewModel);
		}


		[Route("/{userName}")]
		public async Task<ActionResult> UserPhotos(string userName)
		{
			var user = await _userService.GetUserByName(userName);
			var photos = _photoService.GetUserPhotos(userName);
			var followersCount = user.Followers.Count;
			var followeesCount = user.Followings.Count;

			var viewModel = new MyPhotosViewModel
			{
				Photos = photos.ToList(),
				UserName = userName,
				FolloweesCount = followeesCount,
				FollowersCount = followersCount
			};

			return View(viewModel);
		}

		[HttpGet]
		[Route("{photoId:int}/comments")]
		public ActionResult GetComments(int photoId)
		{
			var comments = _photoService.GetPhoto(photoId)
				.Comments
				.ToList();

			return PartialView("_Comments", comments);
		}

	}
}
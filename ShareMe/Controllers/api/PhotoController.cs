using Microsoft.AspNetCore.Mvc;
using ShareMe.Core.Models;
using ShareMe.Services;
using System.Threading.Tasks;

namespace ShareMe.Controllers.api
{
	[Route("/api/photos")]
	public class PhotoController : Controller
	{
		private readonly IPhotoService _photoService;
		private readonly IUserService _userService;
		private readonly IRatingService _ratingService;


		public PhotoController(IPhotoService photoService,
			IUserService userService, IRatingService ratingService)
		{
			_photoService = photoService;
			_userService = userService;
			_ratingService = ratingService;
		}

		//TODO: move to ratings controller
		[HttpPost]
		[Route("like/{id:int}")]
		public async Task<ActionResult> LikePhoto(int id)
		{
			var photo = _photoService.GetPhoto(id);

			var user = await _userService.GetUserAsync(HttpContext.User);

			var rating = _ratingService.GetRating(user.Id, id, "Like");

			if (rating != null)
			{
				_ratingService.RemoveRating(rating);
				return Ok();
			}

			var typeId = _ratingService.GetRatingType("Like").Id;

			var like = new Rating
			{
				PhotoId = photo.Id,
				UserId = user.Id,
				TypeId = typeId
			};

			_ratingService.AddRating(like);

			return Ok();
		}

	}
}
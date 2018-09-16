using Microsoft.AspNetCore.Mvc;
using ShareMe.Core.Models;
using ShareMe.Dtos;
using ShareMe.Services;
using ShareMe.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using ShareMe.Services.Interfaces;

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
				return BadRequest();

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

		//TODO: move to ratings controller
		[HttpDelete]
		[Route("dislike/{id:int}")]
		public async Task<ActionResult> DislikePhoto(int id)
		{
			var user = await _userService.GetUserAsync(HttpContext.User);
			var rating = _ratingService.GetRating(user.Id, id, "Like");

			if (rating == null) return NotFound();

			_ratingService.RemoveRating(rating);
			return Ok();
		}

		[HttpGet]
		[Route("GetLikers/{photoId:int}")]
		public async Task<ActionResult> GetLikers(int photoId)
		{
			var photo = _photoService.GetPhoto(photoId);
			var user = await _userService.GetUserByName(photo.User.UserName);
			var likersIds = photo.Ratings
				.Select(f => f.UserId)
				.ToList();

			var likers = photo.Ratings
				.Select(r => r.User)
				.ToList();

			var followings = user.Followings
				.Where(f => likersIds.Contains(f.FolloweeId))
				.ToLookup(f => f.FolloweeId);
			
			var viewModel = new UserListViewModel
			{
				Followings = followings,
				UserId = user.Id,
				Users = likers
			};

			return PartialView("_UserList", viewModel);
		}

		[HttpPost]
		[Route("comment")]
		public ActionResult AddComment(CommentDto dto)
		{
			var comment = new Comment
			{
				PhotoId	= dto.PhotoId,
				UserId = dto.CommenterId,
				Message = dto.Message
			};

			_photoService.AddComment(comment);

			return Ok();
		}
	}
}
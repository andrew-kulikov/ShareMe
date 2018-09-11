using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShareMe.Core.Models;
using ShareMe.Services;
using System.Threading.Tasks;

namespace ShareMe.Controllers.api
{
	[Produces("application/json")]
	[Route("api/followings")]
	public class FollowingsController : Controller
	{
		private readonly IUserService _userService;
		private readonly IFollowingService _followingService;

		public FollowingsController(IFollowingService followingService, IUserService userService)
		{
			_userService = userService;
			_followingService = followingService;
		}

		[HttpPost]
		[Route("add/{followeeId}")]
		public async Task<ActionResult> Follow(string followeeId)
		{
			var follower = await _userService.GetUserAsync(HttpContext.User);

			var following = new Following
			{
				FolloweeId = followeeId,
				FollowerId = follower.Id
			};

			_followingService.SaveFollowing(following);

			return Ok();
		}

		[HttpDelete]
		[Route("delete/{followeeId}")]
		public async Task<ActionResult> Unfollow(string followeeId)
		{
			var follower = await _userService.GetUserAsync(HttpContext.User);

			var following = _followingService.GetFollowing(follower.Id, followeeId);

			if (following == null)
				return NotFound();

			_followingService.RemoveFollowing(following);

			return Ok();
		}

	}
}
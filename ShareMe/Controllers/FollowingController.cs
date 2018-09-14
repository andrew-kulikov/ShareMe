using Microsoft.AspNetCore.Mvc;
using ShareMe.Services;
using ShareMe.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace ShareMe.Controllers
{
	[Route("followings")]
	public class FollowingController : Controller
	{
		private readonly IUserService _userService;
		private readonly IFollowingService _followingService;

		public FollowingController(IFollowingService followingService, IUserService userService)
		{
			_userService = userService;
			_followingService = followingService;
		}

		[HttpGet]
		[Route("{userName}/followers")]
		public async Task<ActionResult> GetFollowers(string userName)
		{
			var user = await _userService.GetUserByName(userName);
			var followers = user.Followers
				.Select(f => f.Follower)
				.ToList();

			var followingIds = user.Followings.Select(f => f.FolloweeId);

			var followings = user.Followers
				.Where(f => followingIds.Contains(f.FollowerId))
				.ToLookup(f => f.FollowerId);
			
			var viewModel = new UserListViewModel
			{
				Followings = followings,
				UserId = user.Id,
				Users = followers
			};

			return PartialView("_UserList", viewModel);
		}

		[HttpGet]
		[Route("{userName}/followees")]
		public async Task<ActionResult> GetFollowees(string userName)
		{
			var user = await _userService.GetUserByName(userName);
			var followings = user.Followings
				.ToLookup(f => f.FolloweeId);

			var followees = user.Followings
				.Select(f => f.Followee)
				.ToList();

			var viewModel = new UserListViewModel
			{
				Followings = followings,
				UserId = user.Id,
				Users = followees
			};

			return PartialView("_UserList", viewModel);
		}
	}
}
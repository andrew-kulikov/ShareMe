using Microsoft.AspNetCore.Mvc;
using ShareMe.Services;
using ShareMe.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace ShareMe.Controllers
{
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
		[Route("/Following/GetFollowers/{userName}")]
		public async Task<ActionResult> GetFollowers(string userName)
		{
			var user = await _userService.GetUserByName(userName);
			var followings = user.Followers
				.ToLookup(f => f.FollowerId);
			var followers = user.Followers
				.Select(f => f.Follower)
				.ToList();

			var viewModel = new UserListViewModel
			{
				Followings = followings,
				UserId = user.Id,
				Users = followers
			};

			return PartialView("_UserList", viewModel);
		}

		[HttpGet]
		[Route("/Following/GetFollowees/{userName}")]
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
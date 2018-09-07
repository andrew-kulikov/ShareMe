using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShareMe.Core;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using ShareMe.Core.Models;

namespace ShareMe.Controllers.api
{
	[Produces("application/json")]
	[Route("/api/photos")]
	public class PhotoController : Controller
	{
		private ShareMeDbContext _context;
		private readonly UserManager<AspNetUsers> _userManager;

		public PhotoController(UserManager<AspNetUsers> userManager)
		{
			_context = new ShareMeDbContext(new DbContextOptions<ShareMeDbContext>());
			_userManager = userManager;

		}

		[HttpPost("add")]
		public async Task<ActionResult> AddPhoto([FromBody]string url)
		{
			var userName = User.Identity.Name;

			var user = await _userManager.GetUserAsync(HttpContext.User);

			return Ok(user);
		}

		[Authorize]
		[HttpGet]
		public async Task<ActionResult> UserCheck()
		{
			var userName = User.Identity.Name;
			var user = await _userManager.GetUserAsync(HttpContext.User);

			return Ok(user);
		}

	}
}
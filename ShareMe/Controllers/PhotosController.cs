using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShareMe.Core;
using ShareMe.Core.Models;
using ShareMe.ViewModels.PhotoViewModels;

namespace ShareMe.Controllers
{
	public class PhotosController : Controller
	{
		private readonly ShareMeDbContext _context;
		private readonly UserManager<AspNetUsers> _userManager;
		private IConfiguration _configuration;

		public PhotosController(UserManager<AspNetUsers> userManager, IConfiguration configuration)
		{
			_configuration = configuration;

			var optionsBuilder = new DbContextOptionsBuilder<ShareMeDbContext>();
			optionsBuilder.UseSqlServer(_configuration.GetSection("ConnectionStrings")["DefaultConnection"]);
			_context = new ShareMeDbContext(optionsBuilder.Options);
			_userManager = userManager;
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

			var user = await _userManager.GetUserAsync(HttpContext.User);

			var photo = new Photo
			{
				Url = viewModel.Url,
				Description = viewModel.Description,
				UserId = user.Id
			};

			_context.Photos.Add(photo);
			await _context.SaveChangesAsync();

			return RedirectToAction("Index", "Home");
		}
	}
}
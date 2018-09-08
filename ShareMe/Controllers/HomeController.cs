using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShareMe.Core;
using ShareMe.Core.Models;
using ShareMe.ViewModels;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ShareMe.Controllers
{
	public class HomeController : Controller
	{
		private readonly ShareMeDbContext _context;
		private readonly UserManager<AspNetUsers> _userManager;
		private IConfiguration _configuration;

		public HomeController(UserManager<AspNetUsers> userManager, IConfiguration configuration)
		{
			_configuration = configuration;

			var optionsBuilder = new DbContextOptionsBuilder<ShareMeDbContext>();
			optionsBuilder.UseSqlServer(_configuration.GetSection("ConnectionStrings")["DefaultConnection"]);
			_context = new ShareMeDbContext(optionsBuilder.Options);
			_userManager = userManager;
		}

		public async Task<IActionResult> Index()
		{
			var userId = (await _userManager.GetUserAsync(HttpContext.User)).Id;
			var photos = _context.Photos
				.Where(p => p.UserId == userId)
				.ToList();

			return View("Photos", photos);
		}

		public IActionResult About()
		{
			ViewData["Message"] = "Your application description page.";

			return View();
		}

		public IActionResult Contact()
		{
			ViewData["Message"] = "Your contact page.";

			return View();
		}

		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShareMe.Controllers
{
	[Authorize(Roles = "Admin")]
	public class AdminController : Controller
	{
		[Route("/Account/Admin")]
		public IActionResult Index()
		{
			return View();
		}
	}
}
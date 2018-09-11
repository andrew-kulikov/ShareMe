using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShareMe.Core;
using ShareMe.Core.Models;

namespace ShareMe.Services
{
	public class UserService : IUserService
	{
		private readonly UserManager<AspNetUsers> _userManager;
		private readonly ShareMeDbContext _context;


		public UserService(UserManager<AspNetUsers> userManager, IConfiguration configuration)
		{
			_userManager = userManager;
			var optionsBuilder = new DbContextOptionsBuilder<ShareMeDbContext>();
			optionsBuilder.UseSqlServer(configuration.GetSection("ConnectionStrings")["DefaultConnection"]);
			_context = new ShareMeDbContext(optionsBuilder.Options);
		}

		public Task<AspNetUsers> GetUserAsync(ClaimsPrincipal claim) => _userManager.GetUserAsync(claim);
		public Task<AspNetUsers> GetUserById(string userId) => Task.Run(() => 
			_context.AspNetUsers.SingleOrDefault(u => u.Id == userId));
	}
}

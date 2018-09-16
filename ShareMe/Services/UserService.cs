using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShareMe.Core;
using ShareMe.Core.Models;
using ShareMe.Services.Interfaces;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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

		public Task<AspNetUsers> GetUserAsync(ClaimsPrincipal claim) => Task.Run(() =>
			_context.AspNetUsers
				.Include(u => u.Followers)
					.ThenInclude(f => f.Follower)
				.Include(u => u.Followings)
					.ThenInclude(f => f.Followee)
				.Include(u => u.Photos)
				.Include(u => u.Ratings)
				.Include(u => u.Comments)
					.ThenInclude(c => c.User)
				.SingleOrDefault(u => u.UserName == claim.Identity.Name));

		public Task<AspNetUsers> GetUserByName(string userName) => Task.Run(() =>
			_context.AspNetUsers
				.Include(u => u.Followers)
					.ThenInclude(f => f.Follower)
				.Include(u => u.Followings)
					.ThenInclude(f => f.Followee)
				.Include(u => u.Photos)
				.Include(u => u.Ratings)
				.Include(u => u.Comments)
					.ThenInclude(c => c.User)
				.SingleOrDefault(u => u.UserName == userName));

		public Task<IQueryable<AspNetUsers>> GetAll() => Task.Run(() =>
			_context.AspNetUsers.AsQueryable());

		public Task DeleteUser(AspNetUsers user) => Task.Run(() =>
			_context.AspNetUsers.Remove(user));

		public Task<AspNetUsers> GetUserById(string userId) => Task.Run(() =>
			_context.AspNetUsers.SingleOrDefault(u => u.Id == userId));
	}
}

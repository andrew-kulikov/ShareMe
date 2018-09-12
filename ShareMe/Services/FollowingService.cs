using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShareMe.Core;
using ShareMe.Core.Models;
using System.Linq;

namespace ShareMe.Services
{
	public class FollowingService: IFollowingService
	{
		private readonly ShareMeDbContext _context;

		public FollowingService(IConfiguration configuration)
		{
			var optionsBuilder = new DbContextOptionsBuilder<ShareMeDbContext>();
			optionsBuilder.UseSqlServer(configuration.GetSection("ConnectionStrings")["DefaultConnection"]);
			_context = new ShareMeDbContext(optionsBuilder.Options);
		}

		public Following GetFollowing(string followerId, string followeeId) =>
			_context.Followings.FirstOrDefault(f => f.FolloweeId == followeeId &&
			                                        f.FollowerId == followerId);

		public Following GetFollowingById(int id) =>
			_context.Followings.FirstOrDefault(f => f.Id == id);

		public IQueryable<Following> GetUserFollowings(string userId) =>
			_context.Followings.Where(f => f.FollowerId == userId);

		public IQueryable<Following> GetAllFollowings() => _context.Followings;

		public bool IsFollowing(string followerId, string followeeId) =>
			_context.Followings.Any(f => f.FolloweeId == followeeId &&
			                             f.FollowerId == followerId);

		public void SaveFollowing(Following following)
		{
			_context.Followings.Add(following);
			_context.SaveChanges();
		}

		public void RemoveFollowing(Following following)
		{
			_context.Followings.Remove(following);
			_context.SaveChanges();
		}
	}
}

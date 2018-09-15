using System.Linq;
using ShareMe.Core.Models;

namespace ShareMe.Services.Interfaces
{
	public interface IFollowingService
	{
		Following GetFollowing(string followerId, string followeeId);
		Following GetFollowingById(int id);
		IQueryable<Following> GetUserFollowings(string userId);
		IQueryable<Following> GetAllFollowings();
		bool IsFollowing(string followerId, string followeeId);

		void SaveFollowing(Following following);
		void RemoveFollowing(Following following);
	}
}

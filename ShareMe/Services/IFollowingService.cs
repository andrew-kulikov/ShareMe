using ShareMe.Core.Models;
using System.Linq;

namespace ShareMe.Services
{
	public interface IFollowingService
	{
		Following GetFollowing(string followerId, string followeeId);
		Following GetFollowingById(int id);
		IQueryable<Following> GetUserFollowings(string userId);
		bool IsFollowing(string followerId, string followeeId);

		void SaveFollowing(Following following);
		void RemoveFollowing(Following following);
	}
}

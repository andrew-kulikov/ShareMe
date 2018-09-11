using ShareMe.Core.Models;
using System.Linq;

namespace ShareMe.Services
{
	public interface IFollowingService
	{
		Following GetFollowing(string followerId, string followeeId);
		Following GetFollowingById(int id);
		IQueryable<Following> GetUserFollowings(string userId);

		void SaveFollowing(Following following);
		void RemoveFollowing(Following following);
	}
}

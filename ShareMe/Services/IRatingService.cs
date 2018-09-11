using ShareMe.Core.Models;
using System.Collections.Generic;

namespace ShareMe.Services
{
	public interface IRatingService
	{
		RatingType GetRatingType(string name);
		Rating GetRating(int id);
		ICollection<Rating> GetPhotoRatings(int photoId);
		void AddRating(Rating rating);
		bool RatingExists(string userId, int photoId, string name);
		Rating GetRating(string userId, int photoId, string name);
		void RemoveRating(Rating rating);
	}
}

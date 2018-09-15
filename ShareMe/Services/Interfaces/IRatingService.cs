using System.Collections.Generic;
using ShareMe.Core.Models;

namespace ShareMe.Services.Interfaces
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

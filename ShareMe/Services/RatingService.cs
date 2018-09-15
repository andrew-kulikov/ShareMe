using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShareMe.Core;
using ShareMe.Core.Models;
using System.Collections.Generic;
using System.Linq;
using ShareMe.Services.Interfaces;

namespace ShareMe.Services
{
	public class RatingService : IRatingService
	{
		private readonly ShareMeDbContext _context;

		public RatingService(IConfiguration configuration)
		{
			var optionsBuilder = new DbContextOptionsBuilder<ShareMeDbContext>();
			optionsBuilder.UseSqlServer(configuration.GetSection("ConnectionStrings")["DefaultConnection"]);
			_context = new ShareMeDbContext(optionsBuilder.Options);
		}

		public RatingType GetRatingType(string name) =>
			_context.RatingTypes.SingleOrDefault(r => r.Name == name);

		public Rating GetRating(int id) => _context.Ratings.SingleOrDefault(r => r.Id == id);

		public ICollection<Rating> GetPhotoRatings(int photoId) =>
			_context.Photos
				.SingleOrDefault(p => p.Id == photoId)
				?.Ratings;

		public void AddRating(Rating rating)
		{
			_context.AspNetUsers.Load();
			_context.Photos.Load();
			_context.PhotoTags.Load();
			_context.Ratings.Add(rating);
			_context.SaveChanges();
		}

		public bool RatingExists(string userId, int photoId, string name) =>
			_context.Ratings.Any(r => r.PhotoId == photoId &&
									  r.UserId == userId &&
									  r.Type.Name == name);

		public Rating GetRating(string userId, int photoId, string name) =>
			_context.Ratings.FirstOrDefault(r => r.PhotoId == photoId &&
			                                     r.UserId == userId &&
			                                     r.Type.Name == name);

		public void RemoveRating(Rating rating)
		{
			_context.Ratings.Remove(rating);
			_context.SaveChanges();
		}
	}
}

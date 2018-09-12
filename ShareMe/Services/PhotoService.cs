using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShareMe.Core;
using ShareMe.Core.Models;
using System.Linq;

namespace ShareMe.Services
{
	public class PhotoService : IPhotoService
	{
		private readonly ShareMeDbContext _context;

		public PhotoService(IConfiguration configuration)
		{
			var optionsBuilder = new DbContextOptionsBuilder<ShareMeDbContext>();
			optionsBuilder.UseSqlServer(configuration.GetSection("ConnectionStrings")["DefaultConnection"]);
			_context = new ShareMeDbContext(optionsBuilder.Options);
		}

		public Photo GetPhoto(int id) => _context.Photos
			.Include(p => p.Ratings)
				.ThenInclude(r => r.User)
			.Include(p => p.Ratings)
				.ThenInclude(r => r.Type)
			.Include(p => p.User)
			.FirstOrDefault(p => p.Id == id);

		public IQueryable<Photo> GetUserPhotos(string userName) => 
			_context.Photos
				.Include(p => p.User)
				.Include(p => p.Comments)
				.Include(p => p.Ratings)
				.Where(p => p.User.UserName == userName);

		public IQueryable<Photo> GetAllPhotos() => 
			_context.Photos
				.Include(p => p.User)
				.Include(p => p.Comments)
				.Include(p => p.Ratings);

		public void AddPhoto(Photo photo)
		{
			_context.Photos.Add(photo);
			_context.SaveChangesAsync();
		}
	}
}

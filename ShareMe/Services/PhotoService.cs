using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShareMe.Core;
using ShareMe.Core.Models;
using System;
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

		public Photo GetPhoto(int id) => _context.Photos.FirstOrDefault(p => p.Id == id);

		public IQueryable<Photo> GetUserPhotos(string userName) => 
			_context.Photos.Where(p => p.User.UserName == userName);

		public void AddPhoto(Photo photo)
		{
			_context.Photos.Add(photo);
			_context.SaveChangesAsync();
		}
	}
}

using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShareMe.Core;
using ShareMe.Core.Models;
using ShareMe.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

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
			.Include(p => p.Comments)
				.ThenInclude(p => p.User)
			.Include(p => p.User)
			.Include(p => p.Tags)
				.ThenInclude(t => t.Tag)
			.FirstOrDefault(p => p.Id == id);

		public IQueryable<Photo> GetUserPhotos(string userName) => 
			_context.Photos
				.Include(p => p.User)
				.Include(p => p.Comments)
					.ThenInclude(p => p.User)
				.Include(p => p.Ratings)
				.Where(p => p.User.UserName == userName);

		public IQueryable<Photo> GetAllPhotos() => 
			_context.Photos
				.Include(p => p.User)
				.Include(p => p.Comments)
				.Include(p => p.Ratings);

		public ICollection<Tag> ParseTags(string tags)
		{
			var splittedTags = tags.Replace(',', ' ').Split(' ');
			foreach (var tag in splittedTags)
			{
				if (!_context.Tags.Any(t => t.Name == tag))
					_context.Tags.Add(new Tag
					{
						Name = tag
					});
			}

			_context.SaveChanges();

			return _context.Tags
				.Where(t => splittedTags.Contains(t.Name))
				.ToList();
		}

		public async Task<ICollection<PhotoTag>> BindTagsAsync(Photo photo, ICollection<Tag> tags)
		{
			var resTags = new List<PhotoTag>();
			foreach (var tag in tags)
			{
				var photoTag = new PhotoTag
				{
					Photo = photo,
					TagId = tag.Id
				};
				_context.PhotoTags.Add(photoTag);
				resTags.Add(photoTag);
			}
			await _context.SaveChangesAsync();
			return resTags;
		}

		public void AddPhoto(Photo photo)
		{
			_context.Photos.Add(photo);
			_context.SaveChanges();
		}

		public void AddComment(Comment comment)
		{
			_context.Comments.Add(comment);
			_context.SaveChanges();
		}
	}
}

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShareMe.Core.Models;

namespace ShareMe.Services.Interfaces
{
	public interface IPhotoService
	{
		Photo GetPhoto(int id);
		IQueryable<Photo> GetUserPhotos(string userName);
		IQueryable<Photo> GetAllPhotos();
		ICollection<Tag> ParseTags(string tags);
		Task<ICollection<PhotoTag>> BindTagsAsync(Photo photo, ICollection<Tag> tags);
		void AddPhoto(Photo photo);
		void AddComment(Comment comment);
	}
}

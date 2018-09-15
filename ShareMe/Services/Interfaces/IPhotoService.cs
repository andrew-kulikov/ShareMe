using System.Linq;
using ShareMe.Core.Models;

namespace ShareMe.Services.Interfaces
{
	public interface IPhotoService
	{
		Photo GetPhoto(int id);
		IQueryable<Photo> GetUserPhotos(string userName);
		IQueryable<Photo> GetAllPhotos();
		void AddPhoto(Photo photo);
		void AddComment(Comment comment);
	}
}

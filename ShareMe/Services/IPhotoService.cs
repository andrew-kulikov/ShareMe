using ShareMe.Core.Models;
using System.Linq;

namespace ShareMe.Services
{
	public interface IPhotoService
	{
		Photo GetPhoto(int id);
		IQueryable<Photo> GetUserPhotos(string userName);
		void AddPhoto(Photo photo);
	}
}

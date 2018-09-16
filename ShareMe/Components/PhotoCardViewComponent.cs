using Microsoft.AspNetCore.Mvc;
using ShareMe.Services.Interfaces;
using System.Threading.Tasks;

namespace ShareMe.Components
{
	public class PhotoCardViewComponent : ViewComponent
	{
		private readonly IPhotoService _photoService;

		public PhotoCardViewComponent(IPhotoService photoService)
		{
			_photoService = photoService;
		}

		public async Task<IViewComponentResult> InvokeAsync(int photoId)
		{
			var photo = await Task.Run(() => _photoService.GetPhoto(photoId));
			return View("PhotoCard", photo);
		}
	}
}

using ShareMe.Core.Models;

namespace ShareMe.ViewModels.PhotoViewModels
{
	public class PhotoDetailsViewModel
	{
		public Photo Photo { get; set; }
		public bool Following { get; set; }
		public bool Liked { get; set; }
	}
}

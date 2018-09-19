using ShareMe.Core.Models;

namespace ShareMe.ViewModels.PhotoViewModels
{
	public class PhotoDetailsViewModel
	{
		public Photo Photo { get; set; }
		public bool Following { get; set; }
		public bool Liked { get; set; }
		public int LikesCount { get; set; }
		public bool IsCreator { get; set; }
		public string UserId { get; set; }
		public string Comment { get; set; }
	}
}

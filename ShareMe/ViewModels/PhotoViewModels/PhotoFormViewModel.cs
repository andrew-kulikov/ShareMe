using System.ComponentModel.DataAnnotations;

namespace ShareMe.ViewModels.PhotoViewModels
{
	public class PhotoFormViewModel
	{
		[Required]
		public string Url { get; set; }

		[StringLength(1000)]
		public string Description { get; set; }
	}
}

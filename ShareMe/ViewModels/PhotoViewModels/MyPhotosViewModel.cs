using ShareMe.Core.Models;
using System.Collections.Generic;

namespace ShareMe.ViewModels.PhotoViewModels
{
	public class MyPhotosViewModel
	{
		public ICollection<Photo> Photos { get; set; }
		public string UserName { get; set; }
		public int FolloweesCount { get; set; }
		public int FollowersCount { get; set; }
	}
}
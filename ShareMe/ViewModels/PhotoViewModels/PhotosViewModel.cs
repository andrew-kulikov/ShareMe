using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShareMe.Core.Models;

namespace ShareMe.ViewModels.PhotoViewModels
{
	public class PhotosViewModel
	{
		public ICollection<Photo> Photos { get; set; }
		public ILookup<string, Following> Followings { get; set; }
	} 
}

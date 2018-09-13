using ShareMe.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace ShareMe.ViewModels
{
	public class UserListViewModel
	{
		public string UserId { get; set; }
		public ILookup<string, Following> Followings { get; set; }
		public ICollection<AspNetUsers> Users { get; set; }
	}
}

using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ShareMe.Core.Models
{
	public class AspNetUsers : IdentityUser
	{
		public AspNetUsers()
		{
			Followings = new HashSet<Following>();
			Followers = new HashSet<Following>();
			Comments = new HashSet<Comment>();
		}

		public ICollection<Following> Followings { get; set; }
		public ICollection<Following> Followers { get; set; }
		public ICollection<Comment> Comments { get; set; }
	}
}

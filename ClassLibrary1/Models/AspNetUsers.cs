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
			Photos = new HashSet<Photo>();
		}

		public virtual ICollection<Following> Followings { get; set; }
		public virtual ICollection<Following> Followers { get; set; }
		public virtual ICollection<Comment> Comments { get; set; }
		public virtual ICollection<Rating> Ratings { get; set; }
		public virtual ICollection<Photo> Photos { get; set; }
	}
}

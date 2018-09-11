using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShareMe.Core.Models
{
	public class Following
	{
		public int Id { get; set; }

		public virtual AspNetUsers Follower { get; set; }
		public virtual AspNetUsers Followee { get; set; }

		[Key]
		[Column(Order = 1)]
		public string FollowerId { get; set; }

		[Key]
		[Column(Order = 2)]
		public string FolloweeId { get; set; }
	}
}

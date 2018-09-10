using System.ComponentModel.DataAnnotations;

namespace ShareMe.Core.Models
{
	public class Comment
	{
		public int Id { get; set; }

		public AspNetUsers User { get; set; }
		public Photo Photo { get; set; }

		[Required]
		public string UserId { get; set; }

		[Required]
		public int PhotoId { get; set; }
	}
}

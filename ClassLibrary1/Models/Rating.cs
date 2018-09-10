using System.ComponentModel.DataAnnotations;

namespace ShareMe.Core.Models
{
	public class Rating
	{
		public int Id { get; set; }

		public AspNetUsers User { get; set; }
		public RatingType Type { get; set; }
		public Photo Photo { get; set; }

		[Required]
		public string UserId { get; set; }

		[Required]
		public int TypeId { get; set; }

		[Required]
		public int PhotoId { get; set; }
	}
}

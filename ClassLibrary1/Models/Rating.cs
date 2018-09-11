using System.ComponentModel.DataAnnotations;

namespace ShareMe.Core.Models
{
	public class Rating
	{
		public Rating()
		{
			User = new AspNetUsers();
			Type = new RatingType();
			Photo = new Photo();
		}

		public int Id { get; set; }

		public virtual AspNetUsers User { get; set; }
		public virtual RatingType Type { get; set; }
		public virtual Photo Photo { get; set; }

		[Required]
		public string UserId { get; set; }

		[Required]
		public int TypeId { get; set; }

		[Required]
		public int PhotoId { get; set; }
	}
}

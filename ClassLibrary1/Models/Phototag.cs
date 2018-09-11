using System.ComponentModel.DataAnnotations;

namespace ShareMe.Core.Models
{
	public class PhotoTag
	{
		public int Id { get; set; }

		public virtual Photo Photo { get; set; }
		public virtual Tag Tag { get; set; }

		[Required]
		public int PhotoId { get; set; }

		[Required]
		public int TagId { get; set; }
	}
}

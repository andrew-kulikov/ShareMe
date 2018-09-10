using System.ComponentModel.DataAnnotations;

namespace ShareMe.Core.Models
{
	public class PhotoTag
	{
		public int Id { get; set; }

		public Photo Photo { get; set; }
		public Tag Tag { get; set; }

		[Required]
		public int PhotoId { get; set; }

		[Required]
		public int TagId { get; set; }
	}
}

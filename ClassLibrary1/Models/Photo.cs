using System.ComponentModel.DataAnnotations;

namespace ShareMe.Core.Models
{
	public class Photo
	{

		public int Id { get; set; }
	
		[Required]
		public string Url { get; set; }

		[StringLength(1000)]
		public string Description { get; set; }

		public AspNetUsers User { get; set; }

		[Required]
		public string UserId { get; set; }
	}
}

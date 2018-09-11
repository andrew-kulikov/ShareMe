using System.ComponentModel.DataAnnotations;

namespace ShareMe.Core.Models
{
	public class RatingType
	{
		public int Id { get; set; }

		[Required]
		[StringLength(100)]
		public string Name { get; set; }
	}
}

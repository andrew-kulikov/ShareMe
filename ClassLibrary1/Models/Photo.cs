namespace ShareMe.Core.Models
{
	public class Photo
	{
		public string Url { get; set; }
		public string Description { get; set; }

		public ApplicationUser User { get; set; }
	}
}

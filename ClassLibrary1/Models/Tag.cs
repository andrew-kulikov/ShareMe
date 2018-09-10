using System.Collections.Generic;

namespace ShareMe.Core.Models
{
	public class Tag
	{
		public Tag()
		{
			Photos = new HashSet<PhotoTag>();
		}

		public int Id { get; set; }

		public string Name { get; set; }
		public ICollection<PhotoTag> Photos { get; set; }
	}
}

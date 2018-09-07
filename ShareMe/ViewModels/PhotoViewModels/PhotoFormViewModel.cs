using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShareMe.ViewModels.PhotoViewModels
{
    public class PhotoFormViewModel
    {
		[Required]
	    public string Url { get; set; }

		[StringLength(1000)]
	    public string Description { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShareMe.Core.Models;

namespace ShareMe.ViewModels.PhotoViewModels
{
    public class PhotoDetailsViewModel
    {
	    public Photo Photo { get; set; }
	    public bool Following { get; set; }
    }
}

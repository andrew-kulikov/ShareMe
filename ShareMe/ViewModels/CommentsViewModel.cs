using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShareMe.Core.Models;

namespace ShareMe.ViewModels
{
    public class CommentsViewModel
    {
	    public ICollection<Comment> Comments { get; set; }
    }
}

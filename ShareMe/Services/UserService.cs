using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ShareMe.Core.Models;

namespace ShareMe.Services
{
	public class UserService : IUserService
	{
		private readonly UserManager<AspNetUsers> _userManager;

		public UserService(UserManager<AspNetUsers> userManager)
		{
			_userManager = userManager;
		}

		public Task<AspNetUsers> GetUserAsync(ClaimsPrincipal claim) => _userManager.GetUserAsync(claim);

	}
}

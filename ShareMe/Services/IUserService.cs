using ShareMe.Core.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ShareMe.Services
{
	public interface IUserService
	{
		Task<AspNetUsers> GetUserAsync(ClaimsPrincipal claim);
		Task<AspNetUsers> GetUserById(string userId);
	}
}

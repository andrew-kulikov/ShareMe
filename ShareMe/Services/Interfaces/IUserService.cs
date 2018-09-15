using System.Security.Claims;
using System.Threading.Tasks;
using ShareMe.Core.Models;

namespace ShareMe.Services.Interfaces
{
	public interface IUserService
	{
		Task<AspNetUsers> GetUserAsync(ClaimsPrincipal claim);
		Task<AspNetUsers> GetUserById(string userId);
		Task<AspNetUsers> GetUserByName(string userName);
	}
}

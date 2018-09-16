using ShareMe.Core.Models;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ShareMe.Services.Interfaces
{
	public interface IUserService
	{
		Task<AspNetUsers> GetUserAsync(ClaimsPrincipal claim);
		Task<AspNetUsers> GetUserById(string userId);
		Task<AspNetUsers> GetUserByName(string userName);
		Task<IQueryable<AspNetUsers>> GetAll();
		Task DeleteUser(AspNetUsers user);
	}
}

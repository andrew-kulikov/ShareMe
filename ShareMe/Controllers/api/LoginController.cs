using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShareMe.Core.Models;
using ShareMe.ViewModels.AccountViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ShareMe.Controllers.api
{
	[Produces("application/json")]
	[Route("api/Login")]
	public class LoginController : Controller
	{
		private readonly UserManager<AspNetUsers> _userManager;
		private readonly SignInManager<AspNetUsers> _signInManager;
		private readonly IConfiguration _configuration;


		public LoginController(UserManager<AspNetUsers> userManager, SignInManager<AspNetUsers> signInManager,
			IConfiguration configuration)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_configuration = configuration;
		}

		[AllowAnonymous]
		[HttpPost]
		[Route("GenerateToken")]
		public async Task<IActionResult> GenerateToken([FromBody] LoginViewModel model)
		{
			if (!ModelState.IsValid) return BadRequest("Could not create token");
			var user = await _userManager.FindByEmailAsync(model.Email);

			if (user == null) return BadRequest("Could not create token");
			var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
			if (!result.Succeeded) return BadRequest("Could not create token");
			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.Id),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(JwtRegisteredClaimNames.Email, user.Email),
				new Claim("Foo", "Bar")
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(_configuration["Tokens:Issuer"],
				_configuration["Tokens:Issuer"],
				claims,
				expires: DateTime.Now.AddMinutes(30),
				signingCredentials: creds);

			return Ok(new {token = new JwtSecurityTokenHandler().WriteToken(token)});
		}

		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new[] {"value1", "value2"};
		}
	}
}
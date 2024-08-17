using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using tasinmazz.Business.Abstract.Interfaces;
using tasinmazz.Entity.Conrete;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System;

namespace tasinmazz.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IConfiguration _configuration;
		private readonly UserInterface _userService;

		public UserController(UserInterface userInterface, IConfiguration configuration)
		{
			_configuration = configuration;
			_userService = userInterface;
		}

		[HttpPost("register")]
		public async Task<ActionResult<User>> Register([FromBody] UserForRegister user)
		{
			if (await _userService.CheckUserAsync(user.username))
			{
				ModelState.AddModelError("Username", "Username already exists");
			}

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var userToCreate = new User
			{
				Name = user.name,
				Email = user.username,
				Role = user.role
			};

			var newUser = await _userService.RegisterAsync(userToCreate, user.password);
			return Ok(newUser);
		}

		[HttpPut]
		public async Task<ActionResult<User>> UpdateUser([FromBody] UserForRegister user, int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var userToUpdate = new User
			{
				Name = user.name,
				Email = user.username,
				Role = user.role
			};

			var updatedUser = await _userService.UpdateUserAsync(userToUpdate, user.password, id);
			return Ok(updatedUser);
		}

		[HttpPost("login")]
		public async Task<ActionResult<string>> Login([FromBody] UserForLogin user)
		{
			var checkUser = await _userService.LoginAsync(user.username, user.password);
			if (checkUser == null)
			{
				return Unauthorized();
			}

			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim(ClaimTypes.NameIdentifier, checkUser.Id.ToString()),
					new Claim(ClaimTypes.Name, checkUser.Email)
				}),
				Expires = DateTime.Now.AddDays(1),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);
			var tokenString = tokenHandler.WriteToken(token);

			return Ok(new { Token = tokenString });
		}

		[HttpGet]
		public async Task<ActionResult<List<User>>> GetUser()
		{
			var userList = await _userService.GetUserAsync();
			return Ok(userList);
		}

		[HttpGet("GetUserRole")]
		public async Task<ActionResult<string>> GetUserRoleById(int id)
		{
			var user = await _userService.GetUserByIdAsync(id);
			if (user == null)
			{
				return NotFound();
			}
			return Ok(user.Role);
		}

		[HttpGet("GetUserIp")]
		public ActionResult<string> GetUserIp()
		{
			var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
			return Ok(ipAddress);
		}

		[HttpGet("GetUserById")]
		public async Task<ActionResult<User>> GetUserById(int id)
		{
			var user = await _userService.GetUserByIdAsync(id);
			if (user == null)
			{
				return NotFound();
			}
			return Ok(user);
		}

		[HttpGet("GetUserByString")]
		public async Task<ActionResult<List<User>>> GetUserByString(string secenek, string deger)
		{
			var users = await _userService.GetUserByStringAsync(secenek, deger);
			return Ok(users);
		}

		[HttpDelete]
		public async Task<ActionResult<User>> DeleteUser(int id)
		{
			var user = await _userService.DeleteUserAsync(id);
			if (user == null)
			{
				return NotFound();
			}
			return Ok(user);
		}
	}
}
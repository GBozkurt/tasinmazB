using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using tasinmazz.Business.Abstract.Interfaces;
using tasinmazz.Business.Conrete.Services;
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
		private  UserInterface _userService;
		public UserController(UserInterface userInterface, IConfiguration configuration)
		{
			_configuration = configuration;
			_userService = userInterface;
		}
		[HttpPost("register")]
		public ActionResult<User> Register([FromBody] UserForRegister user)
		{
			if (_userService.CheckUser(user.username))
			{
				ModelState.AddModelError("Username", "Username already exists");
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var userToCreate = new User
			{
				Name= user.name,
				Email = user.username,
				Role = user.role
			};
			var newUser = _userService.Register(userToCreate,user.password);
			return Ok(newUser);
		}

		[HttpPut]
		public ActionResult<User> UpdateUser([FromBody] UserForRegister user,int id)
		{
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
			var newUser = _userService.UpdateUser(userToCreate, user.password,id);
			return Ok(newUser);
		}

		[HttpPost("login")]
		public ActionResult<string> Login([FromBody] UserForLogin user)
		{
			var checkUser = _userService.Login(user.username,user.password);
			if (checkUser ==null)
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
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha512Signature)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);
			var tokenString =  tokenHandler.WriteToken(token);


			return Ok(new { Token = tokenString });
		}


		[HttpGet]
		public ActionResult<List<User>> GetUser()
		{
			var userList = _userService.GetUser();
			return Ok(userList);
		}

		[HttpGet("GetUserRole")]
		public ActionResult <String> GetUserRoleById(int id)
		{
			string userList = _userService.GetUserById(id).Role;
			return Ok(userList);
		}

		[HttpGet("GetUserIp")]
		public ActionResult<String> GetUserIp()
		{
			var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
			return Ok(ipAddress);
		}


		[HttpGet("GetUserById")]
		public ActionResult<User> GetUserById(int id)
		{
			var user = _userService.GetUserById(id);
			return Ok(user);
		}

		[HttpGet("GetUserByString")]
		public ActionResult<User> GetUserByString(string secenek,string deger)
		{
			var users= _userService.GetUserByString(secenek, deger);
			return Ok(users);
		}

		[HttpDelete]
		public ActionResult DeleteUser(int id)
		{
			var a =_userService.DeleteUser(id);
			return Ok(a);
		}
	}
}

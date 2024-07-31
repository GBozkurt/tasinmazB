namespace tasinmazz.Business.Conrete.Services
{
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Mvc;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Security.Cryptography;
	using System.Text;
	using tasinmazz.Business.Abstract.Interfaces;
	using tasinmazz.DataAccess.Conrete;
	using tasinmazz.Entity.Conrete;

	public class UserService : UserInterface
	{
		private Context _context;
		public UserService(Context context)
		{
			_context = context;
		}
		
		public User Register(User userDetails,string password)
		{
			byte[] passwordHash, passwordSalt;
			if (CheckUser(userDetails.Email) == false)
			{
				CalculateSHA256(password, out passwordHash, out passwordSalt);
				userDetails.PasswordHash = passwordHash;
				userDetails.PasswordSalt = passwordSalt;

				_context.User.Add(userDetails);
				_context.SaveChanges();

				return userDetails;
			}
			return null;
		}

		public User UpdateUser(User userDetails, string password,int id)
		{
			byte[] passwordHash, passwordSalt;
			var  user = _context.User.Where(x=>x.Id==id).FirstOrDefault();
			if(user.Email==userDetails.Email)
			{
				CalculateSHA256(password, out passwordHash, out passwordSalt);
				user.Name = userDetails.Name;
				user.Email = userDetails.Email;
				user.PasswordHash = passwordHash;
				user.PasswordSalt = passwordSalt;
				user.Role = userDetails.Role;
				_context.SaveChanges();

				return user;
			}
			else
			{
				if (CheckUser(userDetails.Email) == false)
				{
					CalculateSHA256(password, out passwordHash, out passwordSalt);
					user.Name = userDetails.Name;
					user.Email = userDetails.Email;
					user.PasswordHash = passwordHash;
					user.PasswordSalt = passwordSalt;
					user.Role = userDetails.Role;

					_context.SaveChanges();

					return user;
				}
				return null;
			}
		}

		public bool CheckUser(string username)
		{
			if (_context.User.Any(x => x.Email == username)) { return true; }
			return false;
		}

		public void CalculateSHA256(string password, out byte[] passwordHash, out byte[] passwordSalt)
		{
			using (var hmac = new HMACSHA512())
			{
				passwordSalt = hmac.Key;
				passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
			}
		}

		public User Login(string username,string password)
		{
			var user = _context.User.FirstOrDefault( x=>x.Email==username);
			if (user == null) { return null; }
			if (!VerifyPasswordHash(password,user.PasswordHash,user.PasswordSalt)) { return null; }
			return user;
		}

		private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
		{
			using (var hmac = new HMACSHA512(passwordSalt))
			{
				var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
				for (int i = 0; i < computedHash.Length; i++)
				{
					if (computedHash[i] != passwordHash[i])
					return false;
				}
				return true;
			}
		}


		public List<User> GetUser()
		{
			return _context.User.ToList();
		}

		public User GetUserById(int id)
		{
			return _context.User.FirstOrDefault(x => x.Id == id);
		}

		public List<User> GetUserByString(string secenek,string deger)
		{
			var users= _context.User.ToList();
			switch (secenek.ToLower())
			{
				case "isim":
					users = _context.User.Where(x=> x.Name ==deger).ToList();
					break;
				case "email":
					users = _context.User.Where(x=> x.Email == deger).ToList();
					break;
				case "rol":
					users = _context.User.Where(x=> x.Role== deger).ToList();
					break;
			}
			return users;
		}

		public User DeleteUser(int id)
		{
			var a = _context.User.FirstOrDefault(x=> x.Id== id);
			_context.User.Remove(a);
			_context.SaveChanges();
			return a;
		}
	}
}

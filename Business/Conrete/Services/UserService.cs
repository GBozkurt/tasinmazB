using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using tasinmazz.Business.Abstract.Interfaces;
using tasinmazz.DataAccess.Conrete;
using tasinmazz.Entity.Conrete;

namespace tasinmazz.Business.Conrete.Services
{
	public class UserService : UserInterface
	{
		private readonly Context _context;

		public UserService(Context context)
		{
			_context = context;
		}

		//KULLANICI KAYIT ETME
		public async Task<User> RegisterAsync(User userDetails, string password)
		{
			byte[] passwordHash, passwordSalt;
			if (!await CheckUserAsync(userDetails.Email))
			{
				CalculateSHA256(password, out passwordHash, out passwordSalt);
				userDetails.PasswordHash = passwordHash;
				userDetails.PasswordSalt = passwordSalt;

				_context.User.Add(userDetails);
				await _context.SaveChangesAsync();

				return userDetails;
			}
			return null;
		}

		//KULLANICI GÜNCELLEME
		public async Task<User> UpdateUserAsync(User userDetails, string password, int id)
		{
			byte[] passwordHash, passwordSalt;
			var user = await _context.User.FirstOrDefaultAsync(x => x.Id == id);
			if (user == null)
				return null;

			if (user.Email == userDetails.Email)
			{
				CalculateSHA256(password, out passwordHash, out passwordSalt);
				user.Name = userDetails.Name;
				user.Email = userDetails.Email;
				user.PasswordHash = passwordHash;
				user.PasswordSalt = passwordSalt;
				user.Role = userDetails.Role;
				await _context.SaveChangesAsync();

				return user;
			}
			else
			{
				if (!await CheckUserAsync(userDetails.Email))
				{
					CalculateSHA256(password, out passwordHash, out passwordSalt);
					user.Name = userDetails.Name;
					user.Email = userDetails.Email;
					user.PasswordHash = passwordHash;
					user.PasswordSalt = passwordSalt;
					user.Role = userDetails.Role;

					await _context.SaveChangesAsync();

					return user;
				}
				return null;
			}
		}

		//KULLANICI EMAİLİ VAR MI KONTROLÜ
		public async Task<bool> CheckUserAsync(string username)
		{
			return await _context.User.AnyAsync(x => x.Email == username);
		}

		//PAROLAYI HASH VE SALT HALE GETİRME
		public void CalculateSHA256(string password, out byte[] passwordHash, out byte[] passwordSalt)
		{
			using (var hmac = new HMACSHA512())
			{
				passwordSalt = hmac.Key;
				passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
			}
		}

		//KULLANICI GİRİŞ İŞLEMİ
		public async Task<User> LoginAsync(string username, string password)
		{
			var user = await _context.User.FirstOrDefaultAsync(x => x.Email == username);
			if (user == null || !VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
				return null;

			return user;
		}

		//GİRİLEN PAROLA DOĞRU MU KONTROLÜ
		private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
		{
			using (var hmac = new HMACSHA512(passwordSalt))
			{
				var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
				return computedHash.SequenceEqual(passwordHash);
			}
		}

		//KULLANICILARI LİSTELEME
		public async Task<List<User>> GetUserAsync()
		{
			return await _context.User.ToListAsync();
		}

		//KULLANICILARI ID'YE GÖRE LİSTELEME
		public async Task<User> GetUserByIdAsync(int id)
		{
			return await _context.User.FirstOrDefaultAsync(x => x.Id == id);
		}

		//KULLANICILARI ALANA GÖRE LİSTELEME
		public async Task<List<User>> GetUserByStringAsync(string secenek, string deger)
		{
			IQueryable<User> query = _context.User;

			switch (secenek.ToLower())
			{
				case "isim":
					query = query.Where(x => x.Name == deger);
					break;
				case "email":
					query = query.Where(x => x.Email == deger);
					break;
				case "rol":
					query = query.Where(x => x.Role == deger);
					break;
			}

			return await query.ToListAsync();
		}

		//KULLANICI SİLME
		public async Task<User> DeleteUserAsync(int id)
		{
			var user = await _context.User.FirstOrDefaultAsync(x => x.Id == id);
			if (user == null)
				return null;

			_context.User.Remove(user);
			await _context.SaveChangesAsync();
			return user;
		}
	}
}
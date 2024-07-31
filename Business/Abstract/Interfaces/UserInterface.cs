using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using tasinmazz.Entity.Conrete;

namespace tasinmazz.Business.Abstract.Interfaces
{
	public interface UserInterface
	{
		User Register(User userDetails, string password);
		User Login(string username, string password);
		User UpdateUser(User userDetails, string password, int id);
		bool CheckUser(string username);
		List<User> GetUser();
		User GetUserById(int id);
		User DeleteUser(int id);
		List<User> GetUserByString(string secenek, string deger);

	}
}

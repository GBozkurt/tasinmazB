using System.Collections.Generic;
using System.Threading.Tasks;
using tasinmazz.Entity.Conrete;

namespace tasinmazz.Business.Abstract.Interfaces
{
	public interface UserInterface
	{
		Task<User> RegisterAsync(User userDetails, string password);
		Task<User> LoginAsync(string username, string password);
		Task<User> UpdateUserAsync(User userDetails, string password, int id);
		Task<bool> CheckUserAsync(string username);
		Task<List<User>> GetUserAsync();
		Task<User> GetUserByIdAsync(int id);
		Task<User> DeleteUserAsync(int id);
		Task<List<User>> GetUserByStringAsync(string secenek, string deger);
	}
}
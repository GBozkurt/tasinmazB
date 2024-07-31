using System.ComponentModel.DataAnnotations;

namespace tasinmazz.Entity.Conrete
{
	public class User
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		public byte[] PasswordHash { get; set; }
		[Required]
		public byte[] PasswordSalt { get; set; }
		[Required]
		public string Role { get; set; }
	}
}

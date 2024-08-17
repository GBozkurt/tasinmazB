using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using tasinmazz.Entity.Conrete;

namespace tasinmazz.Business.Abstract.Interfaces
{
	public interface TasinmazInterface
	{
		Task<List<Tasinmaz>> GetAllTasinmazAsync();
		Task<Tasinmaz> GetTasinmazByIdAsync(int id);
		Task<List<Tasinmaz>> GetTasinmazByUserIdAsync(int id);
		Task<List<Tasinmaz>> GetTasinmazByStringAsync(string secenek, string deger);
		Task<Tasinmaz> AddTasinmazAsync([FromBody] Tasinmaz tasinmazDetails);
		Task<Tasinmaz> UpdateTasinmazAsync(int id, [FromBody] Tasinmaz tasinmazDetails);
		Task<Tasinmaz> DeleteTasinmazAsync(int id);
	}
}
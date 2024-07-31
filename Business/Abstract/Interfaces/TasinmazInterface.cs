using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using tasinmazz.Entity.Conrete;

namespace tasinmazz.Business.Abstract.Interfaces
{
	public interface TasinmazInterface
	{
		List<Tasinmaz> GetAllTasinmaz();
		Tasinmaz GetTasinmazById(int id);
		List<Tasinmaz> GetTasinmazByUserId(int id);
		List<Tasinmaz> GetTasinmazByString(string secenek, string deger);
		Tasinmaz AddTasinmaz([FromBody] Tasinmaz tasinmazDetails);
		Tasinmaz UpdateTasinmaz(int id, [FromBody] Tasinmaz tasinmazDetails);
		Tasinmaz DeleteTasinmaz(int id);

		List<string> GetAllAda();
		List<string> GetAllParsel();
		List<string> GetAllNitelik();
	}
}

using System.Collections.Generic;
using tasinmazz.Entity.Conrete;

namespace tasinmazz.Business.Abstract.Interfaces
{
	public interface MahalleInterface
	{


		List<Mahalle> GetAllMahalle();
		Mahalle GetMahalleById(int id);
		Mahalle GetMahalleByAdi(string adi);
		List<Mahalle> GetAllMahalleByIlceId(int id);

	}
}

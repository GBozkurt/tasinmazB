using System.Collections.Generic;
using tasinmazz.Entity.Conrete;

namespace tasinmazz.Business.Abstract.Interfaces
{
	public interface IlceInterface
	{
		List<Ilce> GetAllIlce();
		List<Ilce> GetAllIlceById(int id);
		List<Ilce> GetAllIlceByIlId(int id);
	}
}

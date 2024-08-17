using System.Collections.Generic;
using System.Threading.Tasks;
using tasinmazz.Entity.Conrete;

namespace tasinmazz.Business.Abstract.Interfaces
{
	public interface IlceInterface
	{
		Task<List<Ilce>> GetAllIlceAsync();
		Task<List<Ilce>> GetAllIlceByIdAsync(int id);
		Task<List<Ilce>> GetAllIlceByIlIdAsync(int id);
	}
}
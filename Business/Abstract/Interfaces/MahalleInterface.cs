using System.Collections.Generic;
using System.Threading.Tasks;
using tasinmazz.Entity.Conrete;

namespace tasinmazz.Business.Abstract.Interfaces
{
	public interface MahalleInterface
	{
		Task<List<Mahalle>> GetAllMahalleAsync();
		Task<Mahalle> GetMahalleByIdAsync(int id);
		Task<List<Mahalle>> GetAllMahalleByIlceIdAsync(int id);
	}
}
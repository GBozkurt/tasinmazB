using System.Collections.Generic;
using System.Threading.Tasks;
using tasinmazz.Entity.Conrete;

namespace tasinmazz.Business.Abstract.Interfaces
{
	public interface IllerInterface
	{
		Task<List<Il>> GetAllIlAsync();
		Task<List<Il>> GetAllIlByIdAsync(int id);
	}
}
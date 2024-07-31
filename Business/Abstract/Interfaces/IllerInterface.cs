using System.Collections.Generic;
using tasinmazz.Entity.Conrete;

namespace tasinmazz.Business.Abstract.Interfaces
{
	public interface IllerInterface
	{

		List<Il> GetAllIl();
		List<Il> GetAllIlById(int id);
	}
}

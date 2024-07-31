using System.Collections.Generic;
using System.Linq;
using tasinmazz.Business.Abstract.Interfaces;
using tasinmazz.DataAccess.Conrete;
using tasinmazz.Entity.Conrete;

namespace tasinmazz.Business.Conrete.Services
{
	public class IlService : IllerInterface
	{
		private Context _context;
		public IlService(Context context)
		{
			_context = context;
		}

		public List<Il> GetAllIl()
		{
			return _context.Iller.ToList();
		}

		public List<Il> GetAllIlById(int id)
		{
			return _context.Iller.Where(x => x.Id == id).ToList();
		}
	}
}


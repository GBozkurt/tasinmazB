using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using tasinmazz.Business.Abstract.Interfaces;
using tasinmazz.DataAccess.Conrete;
using tasinmazz.Entity.Conrete;

namespace tasinmazz.Business.Conrete.Services
{
	public class IlceService : IlceInterface
	{
		private Context _context;
		public IlceService(Context context)
		{
			_context = context;
		}



		public List<Ilce> GetAllIlce()
		{
			return _context.Ilceler.Include(t => t.Il).ToList();
		}
		
		public List<Ilce> GetAllIlceById(int id)
		{
			return _context.Ilceler.Include(t => t.Il).Where(x => x.Id == id).ToList();
		}
		public List<Ilce> GetAllIlceByIlId(int id)
		{
			return _context.Ilceler.Include(t => t.Il).Where(x => x.IlId == id).ToList();
		}
	}
}

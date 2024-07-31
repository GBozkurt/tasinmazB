using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using tasinmazz.Business.Abstract.Interfaces;
using tasinmazz.DataAccess.Conrete;
using tasinmazz.Entity.Conrete;

namespace tasinmazz.Business.Conrete.Services
{
	public class MahalleService : MahalleInterface
	{
		private Context _context;
		public MahalleService(Context context)
		{
			_context = context;
		}

		public List<Mahalle> GetAllMahalle()
		{
			return _context.Mahalleler.Include(t => t.Ilce).ThenInclude(l => l.Il).ToList();
		}

		
		public Mahalle GetMahalleById(int id)
		{
			return _context.Mahalleler.Include(t => t.Ilce).ThenInclude(l => l.Il).Where(x => x.Id == id).FirstOrDefault();
		}

		public Mahalle GetMahalleByAdi(string adi)
		{
			return _context.Mahalleler.Where(x => x.MahalleAdi == adi).FirstOrDefault();
		}

		public List<Mahalle> GetAllMahalleByIlceId(int id)
		{
			return _context.Mahalleler.Include(t => t.Ilce).Where(x => x.IlceId == id).ToList();
		}


	}
}

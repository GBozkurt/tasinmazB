using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tasinmazz.Business.Abstract.Interfaces;
using tasinmazz.DataAccess.Conrete;
using tasinmazz.Entity.Conrete;

namespace tasinmazz.Business.Conrete.Services
{
	public class IlceService : IlceInterface
	{
		private readonly Context _context;
		public IlceService(Context context)
		{
			_context = context;
		}

		//İLÇELERİ LİSTELEME
		public async Task<List<Ilce>> GetAllIlceAsync()
		{
			return await _context.Ilceler.Include(t => t.Il).ToListAsync();
		}

		//İLÇELERİ ID'YE GÖRE LİSTELEME
		public async Task<List<Ilce>> GetAllIlceByIdAsync(int id)
		{
			return await _context.Ilceler.Include(t => t.Il).Where(x => x.Id == id).ToListAsync();
		}

		//İLÇELERİ İL ID'YE GÖRE LİSTELEME
		public async Task<List<Ilce>> GetAllIlceByIlIdAsync(int id)
		{
			return await _context.Ilceler.Include(t => t.Il).Where(x => x.IlId == id).ToListAsync();
		}
	}
}
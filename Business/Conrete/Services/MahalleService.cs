using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using tasinmazz.Business.Abstract.Interfaces;
using tasinmazz.DataAccess.Conrete;
using tasinmazz.Entity.Conrete;

namespace tasinmazz.Business.Conrete.Services
{
	public class MahalleService : MahalleInterface
	{
		private readonly Context _context;
		public MahalleService(Context context)
		{
			_context = context;
		}

		//MAHALLELERİ LİSTELEME
		public async Task<List<Mahalle>> GetAllMahalleAsync()
		{
			return await _context.Mahalleler.Include(t => t.Ilce).ThenInclude(l => l.Il).ToListAsync();
		}

		//MAHALLELERİ ID'YE GÖRE LİSTELEME
		public async Task<Mahalle> GetMahalleByIdAsync(int id)
		{
			return await _context.Mahalleler.Include(t => t.Ilce).ThenInclude(l => l.Il).Where(x => x.Id == id).FirstOrDefaultAsync();
		}

		//MAHALLELERİ İLÇE ID'YE GÖRE LİSTELEME
		public async Task<List<Mahalle>> GetAllMahalleByIlceIdAsync(int id)
		{
			return await _context.Mahalleler.Include(t => t.Ilce).Where(x => x.IlceId == id).ToListAsync();
		}
	}
}
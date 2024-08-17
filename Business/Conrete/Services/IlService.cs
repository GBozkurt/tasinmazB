using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using tasinmazz.Business.Abstract.Interfaces;
using tasinmazz.DataAccess.Conrete;
using tasinmazz.Entity.Conrete;

namespace tasinmazz.Business.Conrete.Services
{
	public class IlService : IllerInterface
	{
		private readonly Context _context;
		public IlService(Context context)
		{
			_context = context;
		}

		//İLLERİ LİSTELEME
		public async Task<List<Il>> GetAllIlAsync()
		{
			return await _context.Iller.ToListAsync();
		}

		//İLLERİ İD'YE GÖRE LİSTELEME
		public async Task<List<Il>> GetAllIlByIdAsync(int id)
		{
			return await _context.Iller.Where(x => x.Id == id).ToListAsync();
		}
	}
}
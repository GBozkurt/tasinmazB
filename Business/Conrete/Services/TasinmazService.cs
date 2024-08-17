using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tasinmazz.Business.Abstract.Interfaces;
using tasinmazz.DataAccess.Conrete;
using tasinmazz.Entity.Conrete;

namespace tasinmazz.Business.Conrete.Services
{
	public class TasinmazService : TasinmazInterface
	{
		private readonly Context _context;

		public TasinmazService(Context context)
		{
			_context = context;
		}

		//TAŞINMAZLARI LİSTELEME
		public async Task<List<Tasinmaz>> GetAllTasinmazAsync()
		{
			return await _context.Tasinmaz
				.Include(t => t.Mahalle)
				.ThenInclude(k => k.Ilce)
				.ThenInclude(l => l.Il)
				.ToListAsync();
		}

		//TAŞINMAZLARI ID'YE GÖRE LİSTELEME
		public async Task<Tasinmaz> GetTasinmazByIdAsync(int id)
		{
			return await _context.Tasinmaz
				.Include(t => t.Mahalle)
				.ThenInclude(k => k.Ilce)
				.ThenInclude(l => l.Il)
				.FirstOrDefaultAsync(x => x.Id == id);
		}

		//TAŞINMAZLARI KULLANICI ID'SİNE GÖRE LİSTELEME
		public async Task<List<Tasinmaz>> GetTasinmazByUserIdAsync(int id)
		{
			return await _context.Tasinmaz
				.Include(t => t.Mahalle)
				.ThenInclude(k => k.Ilce)
				.ThenInclude(l => l.Il)
				.Where(x => x.UserId == id)
				.ToListAsync();
		}

		//TAŞINMAZLARI ALANA GÖRE LİSTELEME
		public async Task<List<Tasinmaz>> GetTasinmazByStringAsync(string secenek, string deger)
		{
			IQueryable<Tasinmaz> query = _context.Tasinmaz
				.Include(t => t.Mahalle)
				.ThenInclude(k => k.Ilce)
				.ThenInclude(l => l.Il);

			switch (secenek.ToLower())
			{
				case "il":
					query = query.Where(t => t.Mahalle.Ilce.Il.IlAdi == deger);
					break;
				case "ilce":
					query = query.Where(t => t.Mahalle.Ilce.IlceAdi == deger);
					break;
				case "mahalle":
					query = query.Where(t => t.Mahalle.MahalleAdi == deger);
					break;
				case "ada":
					query = query.Where(t => t.Ada == deger);
					break;
				case "parsel":
					query = query.Where(t => t.Parsel == deger);
					break;
				case "nitelik":
					query = query.Where(t => t.Nitelik == deger);
					break;
				case "koordinatbilgileri":
					query = query.Where(t => t.KoordinatBilgileri == deger);
					break;
			}

			return await query.ToListAsync();
		}

		//TAŞINMMAZ EKLEME
		public async Task<Tasinmaz> AddTasinmazAsync([FromBody] Tasinmaz tasinmazDetails)
		{
			var yeniTasinmaz = new Tasinmaz
			{
				MahalleId = tasinmazDetails.MahalleId,
				Ada = tasinmazDetails.Ada,
				Parsel = tasinmazDetails.Parsel,
				Nitelik = tasinmazDetails.Nitelik,
				KoordinatBilgileri = tasinmazDetails.KoordinatBilgileri,
				UserId = tasinmazDetails.UserId,
			};

			_context.Tasinmaz.Add(yeniTasinmaz);
			await _context.SaveChangesAsync();

			return yeniTasinmaz;
		}

		//TAŞINMAZ GÜNCELLEME
		public async Task<Tasinmaz> UpdateTasinmazAsync(int id, [FromBody] Tasinmaz tasinmazDetails)
		{
			var mevcutTasinmaz = await _context.Tasinmaz.FirstOrDefaultAsync(x => x.Id == id);
			if (mevcutTasinmaz != null)
			{
				mevcutTasinmaz.MahalleId = tasinmazDetails.MahalleId;
				mevcutTasinmaz.Ada = tasinmazDetails.Ada;
				mevcutTasinmaz.Parsel = tasinmazDetails.Parsel;
				mevcutTasinmaz.Nitelik = tasinmazDetails.Nitelik;
				mevcutTasinmaz.KoordinatBilgileri = tasinmazDetails.KoordinatBilgileri;

				await _context.SaveChangesAsync();
			}
			return mevcutTasinmaz;
		}

		//TAŞINMAZ SİLME
		public async Task<Tasinmaz> DeleteTasinmazAsync(int id)
		{
			var silinecekTasinmaz = await _context.Tasinmaz.FirstOrDefaultAsync(x => x.Id == id);
			if (silinecekTasinmaz != null)
			{
				_context.Tasinmaz.Remove(silinecekTasinmaz);
				await _context.SaveChangesAsync();
			}
			return silinecekTasinmaz;
		}
	}
}
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using tasinmazz.Business.Abstract.Interfaces;
using tasinmazz.DataAccess.Conrete;
using tasinmazz.Entity.Conrete;

namespace tasinmazz.Business.Conrete.Services
{
	public class TasinmazService : TasinmazInterface
	{
		//DB CONNECTİON
		private Context _context;
		public TasinmazService(Context context)
		{
			_context = context;
		}

		//TAŞINMAZLARI LİSTELE
		public List<Tasinmaz> GetAllTasinmaz()
		{
			return _context.Tasinmaz.Include(t => t.Mahalle).ThenInclude(k => k.Ilce).ThenInclude(l => l.Il).ToList();
		}


		//TAŞINMAZLARI ID'YE GÖRE LİSTELE
		public Tasinmaz GetTasinmazById(int id)
		{
			return _context.Tasinmaz
				.Include(t => t.Mahalle)
				.ThenInclude(k => k.Ilce)
				.ThenInclude(l => l.Il)
				.FirstOrDefault(x => x.Id == id);
		}

		public List<Tasinmaz> GetTasinmazByUserId(int id)
		{
			return _context.Tasinmaz
				.Include(t => t.Mahalle)
				.ThenInclude(k => k.Ilce)
				.ThenInclude(l => l.Il)
				.Where(x => x.UserId == id).ToList();
		}

		//TAŞINMAZLARI İL,İLÇE VB.'YE GÖRE LİSTELE
		public List<Tasinmaz> GetTasinmazByString(string secenek, string deger)
		{
			
			var TasinmazDetails = _context.Tasinmaz.ToList();
			switch (secenek.ToLower())
			{
				case "il":
					TasinmazDetails = _context.Tasinmaz.Include(t => t.Mahalle).ThenInclude(k => k.Ilce).ThenInclude(l => l.Il).Where(t => t.Mahalle.Ilce.Il.IlAdi == deger).ToList();
					break;
				case "ilce":
					TasinmazDetails = _context.Tasinmaz.Include(t => t.Mahalle).ThenInclude(k => k.Ilce).ThenInclude(l => l.Il).Where(t => t.Mahalle.Ilce.IlceAdi == deger).ToList();
					break;
				case "mahalle":
					TasinmazDetails = _context.Tasinmaz.Include(t => t.Mahalle).ThenInclude(k => k.Ilce).ThenInclude(l => l.Il).Where(t => t.Mahalle.MahalleAdi == deger).ToList();
					break;
				case "ada":
					TasinmazDetails = _context.Tasinmaz.Include(t => t.Mahalle).ThenInclude(k => k.Ilce).ThenInclude(l => l.Il).Where(t => t.Ada == deger).ToList();
					break;
				case "parsel":
					TasinmazDetails = _context.Tasinmaz.Include(t => t.Mahalle).ThenInclude(k => k.Ilce).ThenInclude(l => l.Il).Where(t => t.Parsel == deger).ToList();
					break;
				case "nitelik":
					TasinmazDetails = _context.Tasinmaz.Include(t => t.Mahalle).ThenInclude(k => k.Ilce).ThenInclude(l => l.Il).Where(t => t.Nitelik == deger).ToList();
					break;
				case "koordinatbilgileri":
					TasinmazDetails = _context.Tasinmaz.Include(t => t.Mahalle).ThenInclude(k => k.Ilce).ThenInclude(l => l.Il).Where(t => t.KoordinatBilgileri == deger).ToList();
					break;
			}


			return TasinmazDetails;
		}

		//TAŞINMAZ EKLE
		public Tasinmaz AddTasinmaz([FromBody] Tasinmaz tasinmazDetails)
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
			_context.SaveChanges();

			return yeniTasinmaz;
		}

		//TAŞINMAZ GÜNCELLE
		public Tasinmaz UpdateTasinmaz(int id, [FromBody] Tasinmaz tasinmazDetails)
		{
			var yeniTasinmaz = _context.Tasinmaz.FirstOrDefault(
				x => x.Id == id);
			yeniTasinmaz.MahalleId = tasinmazDetails.MahalleId;
			yeniTasinmaz.Ada = tasinmazDetails.Ada;
			yeniTasinmaz.Parsel = tasinmazDetails.Parsel;
			yeniTasinmaz.Nitelik = tasinmazDetails.Nitelik;
			yeniTasinmaz.KoordinatBilgileri = tasinmazDetails.KoordinatBilgileri;
			_context.SaveChanges();
			return yeniTasinmaz;
		}

		//TAŞINMAZ SİL
		public Tasinmaz DeleteTasinmaz(int id)
		{
			
			var SilinecekTasinmaz = _context.Tasinmaz.FirstOrDefault(
				x => x.Id == id);
			if(SilinecekTasinmaz == null)
			{
				return null;
			}
			
			_context.Remove(SilinecekTasinmaz);
			_context.SaveChanges();
			return SilinecekTasinmaz;
		}

		//ADALARI LİSTELE
		[HttpGet("GetAda")]
		public List<string> GetAllAda()
		{
			return _context.Tasinmaz.Select(x => x.Ada).ToList();
		}

		//NİTELİKLERİ LİSTELE
		[HttpGet("GetNitelik")]
		public List<string> GetAllNitelik()
		{
			return _context.Tasinmaz.Select(x => x.Nitelik).ToList();
		}

		//PARSELLERİ LİSTELE
		[HttpGet("GetParsel")]
		public List<string> GetAllParsel()
		{
			return _context.Tasinmaz.Select(x => x.Parsel).ToList();
		}


	}
}

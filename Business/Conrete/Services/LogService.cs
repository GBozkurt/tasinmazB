using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using tasinmazz.Business.Abstract.Interfaces;
using tasinmazz.DataAccess.Conrete;
using tasinmazz.Entity.Conrete;

namespace tasinmazz.Business.Conrete.Services
{
	public class LogService : LogInterface
	{
		private readonly Context _context;
		public LogService(Context context)
		{
			_context = context;
		}

		//LOGLARI LİSTELEME
		public async Task<List<Log>> GetLogsAsync()
		{
			return await _context.Log.ToListAsync();
		}

		//LOGLARI ID'YE GÖRE LİSTELEME
		public async Task<Log> GetLogByIdAsync(int id)
		{
			return await _context.Log.FirstOrDefaultAsync(x => x.Id == id);
		}

		//LOGLARI ALANLARA GÖRE LİSTELEME
		public async Task<List<Log>> GetLogsByStringAsync(string secenek, string deger)
		{
			var logs = await _context.Log.ToListAsync();

			switch (secenek.ToLower())
			{
				case "kullaniciid":
					logs = await _context.Log.Where(x => x.UserId == Convert.ToInt32(deger)).ToListAsync();
					break;
				case "durum":
					logs = await _context.Log.Where(x => x.Durum == deger).ToListAsync();
					break;
				case "islem":
					logs = await _context.Log.Where(x => x.IslemTip == deger).ToListAsync();
					break;
				case "aciklama":
					logs = await _context.Log.Where(x => x.Aciklama.Contains(deger)).ToListAsync();
					break;
				case "tarih":
					DateTime targetDateTime = DateTime.Parse(deger);
					logs = await _context.Log.Where(x => x.TarihSaat.Year == targetDateTime.Year &&
						x.TarihSaat.Month == targetDateTime.Month &&
						x.TarihSaat.Day == targetDateTime.Day)
						.ToListAsync();
					break;
				case "kullaniciip":
					logs = await _context.Log.Where(x => x.KullaniciIp == deger).ToListAsync();
					break;
			}
			return logs;
		}

		//LOG KAYDI EKLEME
		public async Task<Log> PostLogAsync(Log log)
		{
			_context.Log.Add(log);
			await _context.SaveChangesAsync();
			return log;
		}

		//LOG SİLME
		public async Task<Log> DeleteLogAsync(int id)
		{
			var log = await _context.Log.FirstOrDefaultAsync(x => x.Id == id);
			_context.Log.Remove(log);
			await _context.SaveChangesAsync();
			return log;
		}
	}
}
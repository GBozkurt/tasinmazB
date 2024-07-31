using System;
using System.Collections.Generic;
using System.Linq;
using tasinmazz.Business.Abstract.Interfaces;
using tasinmazz.DataAccess.Conrete;
using tasinmazz.Entity.Conrete;

namespace tasinmazz.Business.Conrete.Services
{
	public class LogService : LogInterface
	{
		private Context _context;
		public LogService(Context context)
		{
			_context = context;
		}


		public List<Log> GetLogs()
		{
			var logs = _context.Log.ToList();
			return logs;
		}

		public Log GetLogById(int id)
		{
			var log = _context.Log.FirstOrDefault(x=> x.Id == id);
			return log;
		}

		public Log PostLog(Log log)
		{
			
			_context.Log.Add(log);
			_context.SaveChanges();
			return log;
		}

		public List<Log> GetLogsByString( string secenek, string deger)
		{
			var logs = _context.Log.ToList();

			switch (secenek.ToLower())
			{
				case "kullaniciid":
					logs = _context.Log.Where(x=>x.UserId==Convert.ToInt32(deger)).ToList();
					break;
				case "durum":
					logs = _context.Log.Where(x=>x.Durum == deger).ToList(); break;
				case "islem":
					logs = _context.Log.Where(x=>x.IslemTip==deger ).ToList(); break;
				case "aciklama":
					logs = _context.Log.Where(x=> x.Aciklama.Contains(deger) ).ToList(); break;
				case "tarih":
					DateTime targetDateTime = DateTime.Parse(deger);
					logs = _context.Log.Where(x => x.TarihSaat.Year == targetDateTime.Year &&
				x.TarihSaat.Month == targetDateTime.Month &&
				x.TarihSaat.Day == targetDateTime.Day)
				.ToList(); break;
				case "kullaniciip":
					logs = _context.Log.Where(x=>x.KullaniciIp==deger).ToList(); break;
			}
			return logs;
		} 

		public Log DeleteLog(int id)
		{
			var log = _context.Log.FirstOrDefault(x => x.Id == id);
			_context.Log.Remove(log);
			_context.SaveChanges();
			return log;	
		}
	}
}

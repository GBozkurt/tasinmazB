using System.Collections.Generic;
using tasinmazz.Entity.Conrete;

namespace tasinmazz.Business.Abstract.Interfaces
{
	public interface LogInterface
	{
		List<Log> GetLogs();

		Log PostLog(Log log);
		Log DeleteLog(int id);
		Log GetLogById(int id);
		List<Log> GetLogsByString(string secenek, string deger);
	}
}

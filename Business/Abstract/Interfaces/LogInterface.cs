using System.Collections.Generic;
using System.Threading.Tasks;
using tasinmazz.Entity.Conrete;

namespace tasinmazz.Business.Abstract.Interfaces
{
	public interface LogInterface
	{
		Task<List<Log>> GetLogsAsync();
		Task<Log> GetLogByIdAsync(int id);
		Task<Log> PostLogAsync(Log log);
		Task<List<Log>> GetLogsByStringAsync(string secenek, string deger);
		Task<Log> DeleteLogAsync(int id);
	}
}
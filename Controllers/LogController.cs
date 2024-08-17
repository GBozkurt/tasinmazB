using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using tasinmazz.Business.Abstract.Interfaces;
using tasinmazz.Entity.Conrete;

namespace tasinmazz.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LogController : ControllerBase
	{
		private readonly LogInterface _logService;
		public LogController(LogInterface logservice)
		{
			_logService = logservice;
		}

		[HttpGet]
		public async Task<ActionResult<List<Log>>> GetLogsAsync()
		{
			var logs = await _logService.GetLogsAsync();
			return logs;
		}

		[HttpGet("GetLogById")]
		public async Task<ActionResult<Log>> GetLogByIdAsync(int id)
		{
			
			var log = await _logService.GetLogByIdAsync(id);
			if (log == null)
			{
				return BadRequest("Log bulunamadı " + id);
			}
			return log;
		}

		[HttpGet("GetLogsByString")]
		public async Task<ActionResult<List<Log>>> GetLogsByStringAsync(string secenek, string deger)
		{
			if (secenek == null && deger == null)
			{
				return BadRequest();
			}
			var logs = await _logService.GetLogsByStringAsync(secenek, deger);
			if (logs == null) { return BadRequest(); }
			return logs;
		}

		[HttpDelete]
		public async Task<ActionResult<Log>> DeleteLogsAsync(int id)
		{
			var log = await _logService.DeleteLogAsync(id);
			return Ok(log);
		}

		[HttpPost]
		public async Task<ActionResult<Log>> PostLogsAsync([FromBody] Log log)
		{
			if (log == null)
			{
				return BadRequest();
			}
			log.TarihSaat = DateTime.Now;
			var newLog = await _logService.PostLogAsync(log);
			return newLog;
		}
	}
}
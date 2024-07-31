using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Operation.Valid;
using System;
using System.Collections.Generic;
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
		public ActionResult<List<Log>> GetLogs()
		{
			var logs = _logService.GetLogs();
			return logs;
		}

		[HttpPost]
		public ActionResult<Log> PostLogs([FromBody] Log log)
		{

			if (log == null)
			{
				return BadRequest(); 
			}
			log.TarihSaat = DateTime.Now;
			var newLog = _logService.PostLog(log);
			return newLog;
		}

		[HttpGet("GetLogById")]
		public ActionResult<Log> GetLogById(int id)
		{
			if (id == null)
			{
				return BadRequest("Id alınamadı");
			}
			var log = _logService.GetLogById(id);
			if (log==null)
			{
				return BadRequest("Log bulunamadı "+id);
			}
			return(log);
		}

		[HttpGet("GetLogsByString")]
		public ActionResult<List<Log>> GetLogsByString(string secenek,string deger)
		{
			if(secenek == null && deger == null)
			{
				return BadRequest();
			}
			var logs = _logService.GetLogsByString(secenek, deger);
			if (logs == null) { return BadRequest();}
			return logs;
		}

		[HttpDelete]
		public ActionResult<Log> DeleteLogs(int id)
		{
			var log = _logService.DeleteLog(id);
			return Ok(log);
		}
	}
}

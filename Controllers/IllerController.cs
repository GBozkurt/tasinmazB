using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using tasinmazz.Business.Abstract.Interfaces;
using tasinmazz.DataAccess.Conrete;
using tasinmazz.Entity.Conrete;

namespace tasinmazz.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class IllerController : ControllerBase
	{
		private readonly IllerInterface _illerService;
		public IllerController(IllerInterface illerService)
		{
			_illerService = illerService;
		}

		[HttpGet]
		public ActionResult<List<Il>> GetAllIl()
		{
			var iller = _illerService.GetAllIl();
			return Ok(iller);
		}
		[HttpGet("GetIlById")]
		public ActionResult<List<Il>>GetAllIlById(int id)
		{
			var il = _illerService.GetAllIlById(id);
			return Ok(il);
		}
	}
	
}

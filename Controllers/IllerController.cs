using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using tasinmazz.Business.Abstract.Interfaces;
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
		public async Task<ActionResult<List<Il>>> GetAllIl()
		{
			var iller = await _illerService.GetAllIlAsync();
			return Ok(iller);
		}

		[HttpGet("GetIlById")]
		public async Task<ActionResult<List<Il>>> GetAllIlById(int id)
		{
			var il = await _illerService.GetAllIlByIdAsync(id);
			return Ok(il);
		}
	}
}
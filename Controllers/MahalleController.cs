using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using tasinmazz.Business.Abstract.Interfaces;
using tasinmazz.Entity.Conrete;

namespace tasinmazz.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MahalleController : ControllerBase
	{
		private readonly MahalleInterface _mahalleService;
		public MahalleController(MahalleInterface mahalleService)
		{
			_mahalleService = mahalleService;
		}

		[HttpGet]
		public async Task<ActionResult<List<Mahalle>>> GetAllMahalle()
		{
			var mahalle = await _mahalleService.GetAllMahalleAsync();
			return Ok(mahalle);
		}

		[HttpGet("GetMahalleById")]
		public async Task<ActionResult<Mahalle>> GetMahalleById(int id)
		{
			var mahalle = await _mahalleService.GetMahalleByIdAsync(id);
			return Ok(mahalle);
		}

		[HttpGet("GetMahalleByIlceId")]
		public async Task<ActionResult<List<Mahalle>>> GetAllMahalleByIlceId(int id)
		{
			var mahalle = await _mahalleService.GetAllMahalleByIlceIdAsync(id);
			return Ok(mahalle);
		}
	}
}
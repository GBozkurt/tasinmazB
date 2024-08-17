using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using tasinmazz.Business.Abstract.Interfaces;
using tasinmazz.Entity.Conrete;

namespace tasinmazz.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class IlceController : ControllerBase
	{
		private readonly IlceInterface _ilceInterface;
		public IlceController(IlceInterface ilceInterface)
		{
			_ilceInterface = ilceInterface;
		}

		[HttpGet]
		public async Task<ActionResult<List<Ilce>>> GetAllIlce()
		{
			var ilceler = await _ilceInterface.GetAllIlceAsync();
			return Ok(ilceler);
		}

		[HttpGet("GetIlceById")]
		public async Task<ActionResult<List<Ilce>>> GetAllIlceById(int id)
		{
			var ilce = await _ilceInterface.GetAllIlceByIdAsync(id);
			return Ok(ilce);
		}

		[HttpGet("GetIlceByIlId")]
		public async Task<ActionResult<List<Ilce>>> GetAllIlceByIlId(int id)
		{
			var ilce = await _ilceInterface.GetAllIlceByIlIdAsync(id);
			return Ok(ilce);
		}
	}
}
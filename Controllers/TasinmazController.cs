using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using tasinmazz.Business.Abstract.Interfaces;
using tasinmazz.Entity.Conrete;

namespace tasinmazz.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TasinmazController : ControllerBase
	{
		private readonly TasinmazInterface _tasinmazService;

		public TasinmazController(TasinmazInterface tasinmazService)
		{
			_tasinmazService = tasinmazService;
		}

		[HttpGet]
		public async Task<ActionResult<List<Tasinmaz>>> GetAllTasinmaz()
		{
			var tasinmazList = await _tasinmazService.GetAllTasinmazAsync();
			return Ok(tasinmazList);
		}

		[HttpGet("GetTasinmazById")]
		public async Task<ActionResult<Tasinmaz>>	GetTasinmazById(int id)
		{
			if (id == 0)
			{
				return BadRequest();
			}

			var tasinmaz = await _tasinmazService.GetTasinmazByIdAsync(id);

			if (tasinmaz == null)
			{
				return NotFound();
			}

			return Ok(tasinmaz);
		}

		[HttpGet("GetTasinmazByUserId")]
		public async Task<ActionResult<List<Tasinmaz>>> GetTasinmazByUserId(int id)
		{
			if (id == 0)
			{
				return BadRequest();
			}

			var tasinmaz = await _tasinmazService.GetTasinmazByUserIdAsync(id);

			if (tasinmaz == null)
			{
				return NotFound();
			}

			return Ok(tasinmaz);
		}

		[HttpGet("GetTasinmazByString")]
		public async Task<ActionResult<List<Tasinmaz>>> GetTasinmazByString(string secenek, string deger)
		{
			if (string.IsNullOrEmpty(deger) || string.IsNullOrEmpty(secenek))
			{
				return BadRequest();
			}

			var tasinmazDetails = await _tasinmazService.GetTasinmazByStringAsync(secenek, deger);

			if (tasinmazDetails == null)
			{
				return NotFound();
			}

			return Ok(tasinmazDetails);
		}

		[HttpPost("AddTasinmaz")]
		public async Task<ActionResult<Tasinmaz>> AddTasinmaz([FromBody] Tasinmaz tasinmazDetails)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			var yeniTasinmaz = await _tasinmazService.AddTasinmazAsync(tasinmazDetails);
			return Ok(yeniTasinmaz);
		}

		[HttpPut("UpdateTasinmaz")]
		public async Task<ActionResult<Tasinmaz>> UpdateTasinmaz(int id, [FromBody] Tasinmaz tasinmazDetails)
		{
			if (tasinmazDetails == null)
			{
				return BadRequest(ModelState);
			}

			var updatedTasinmaz = await _tasinmazService.UpdateTasinmazAsync(id, tasinmazDetails);
			if (updatedTasinmaz == null)
			{
				return NotFound();
			}

			return Ok(updatedTasinmaz);
		}

		[HttpDelete("DeleteTasinmaz")]
		public async Task<ActionResult<Tasinmaz>> DeleteTasinmaz(int id)
		{
			var silinecekTasinmaz = await _tasinmazService.DeleteTasinmazAsync(id);
			if (silinecekTasinmaz == null)
			{
				return NotFound();
			}

			return Ok(silinecekTasinmaz);
		}
	}
}
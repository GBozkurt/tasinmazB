using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using tasinmazz.Business.Abstract.Interfaces;
using tasinmazz.Business.Conrete.Services;
using tasinmazz.DataAccess.Conrete;
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
		public ActionResult<List<Tasinmaz>>GetAllTasinmaz()
		{
			var tasinmazList =_tasinmazService.GetAllTasinmaz();
			return tasinmazList;
		}

		[HttpGet("GetTasinmazById")]
		public ActionResult<Tasinmaz> GetTasinmazById(int id)
		{
			if (id == 0)
			{
				return BadRequest(); ;
			}
			var tasinmaz = _tasinmazService.GetTasinmazById(id);

			if (tasinmaz == null)
			{
				return NotFound();
			}

			return Ok(tasinmaz);
		}

		[HttpGet("GetTasinmazByUserId")]
		public ActionResult<List<Tasinmaz>> GetTasinmazByUserId(int id)
		{
			if (id == 0)
			{
				return BadRequest(); ;
			}
			var tasinmaz = _tasinmazService.GetTasinmazByUserId(id);

			if (tasinmaz == null)
			{
				return NotFound();
			}

			return Ok(tasinmaz);
		}


		[HttpGet("GetTasinmazByString")]
		public ActionResult<List<Tasinmaz>> GetTasinmazByString(string secenek, string deger)
		{
			if (deger == null && secenek == null)
			{
				return BadRequest(); ;
			}
			var TasinmazDetails = _tasinmazService.GetTasinmazByString(secenek,deger);

			if (TasinmazDetails == null)
			{
				return NotFound();
			}

			return TasinmazDetails;
		}

		[HttpPost("AddTasinmaz")]
		public ActionResult<Tasinmaz> AddTasinmaz([FromBody] Tasinmaz tasinmazDetails)
		{
			if (!ModelState.IsValid) {  return BadRequest(); }
			var yeniTasinmaz = _tasinmazService.AddTasinmaz(tasinmazDetails);
			return Ok(yeniTasinmaz);
		}

		[HttpPut("UpdateTasinmaz")]
		public ActionResult<Tasinmaz> UpdateTasinmaz(int id, [FromBody] Tasinmaz tasinmazDetails)
		{
			if (tasinmazDetails == null)
			{
				return BadRequest(ModelState);
			}
			var yeniTasinmaz = _tasinmazService.UpdateTasinmaz(id, tasinmazDetails);
			return Ok(yeniTasinmaz);
		}

		[HttpDelete("DeleteTasinmaz")]
		public ActionResult<Tasinmaz> DeleteTasinmaz(int id)
		{

			var SilinecekTasinmaz = _tasinmazService.DeleteTasinmaz(id);
			if (SilinecekTasinmaz == null)
			{
				return NotFound();
			}
			return Ok(SilinecekTasinmaz);
		}

		[HttpGet("GetAda")]
		public ActionResult<List<string>> GetAllAda()
		{
			var adaList = _tasinmazService.GetAllAda();
			return adaList;
		}

		[HttpGet("GetNitelik")]
		public ActionResult<List<string>> GetAllNitelik()
		{
			var nitelikList = _tasinmazService.GetAllNitelik();
			return nitelikList;
		}

		[HttpGet("GetParsel")]
		public ActionResult<List<string>> GetAllParsel()
		{
			var parselList = _tasinmazService.GetAllParsel();
			return parselList;
		}
	}
}

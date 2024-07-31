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
	public class MahalleController : ControllerBase
	{
		private readonly MahalleInterface _mahalleService;
		public MahalleController(MahalleInterface mahalleService)
		{
			_mahalleService = mahalleService;	
		}

		[HttpGet]
		public ActionResult<List<Mahalle>> GetAllMahalle()
		{
			var mahalle = _mahalleService.GetAllMahalle();
			return Ok(mahalle);
		}

		[HttpGet("GetMahalleById")]
		public ActionResult<Mahalle> GetMahalleById(int id)
		{
			var mahalle = _mahalleService.GetMahalleById(id);
			return Ok(mahalle);
		}

		[HttpGet("GetMahalleByAdi")]
		public ActionResult<Mahalle> GetMahalleByAdi(string adi)
		{
			var mahalle = _mahalleService.GetMahalleByAdi(adi);
			return Ok(mahalle);
		}

		[HttpGet("GetMahalleByIlceId")]
		public ActionResult<List<Mahalle>> GetAllMahalleByIlceId(int id)
		{
			var mahalle = _mahalleService.GetAllMahalleByIlceId(id);
			return Ok(mahalle);
		}
	}
	
}

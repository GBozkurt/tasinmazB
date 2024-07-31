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
	public class IlceController : ControllerBase
	{
		private readonly IlceInterface _ilceInterface;
		public IlceController(IlceInterface ilceInterface)
		{
			_ilceInterface = ilceInterface;
		}

		[HttpGet]
		public ActionResult<List<Ilce>> GetAllIlce()
		{
			var ilceler = _ilceInterface.GetAllIlce();
			return Ok(ilceler);
		}
		[HttpGet("GetIlceById")]
		public ActionResult<List<Ilce>> GetAllIlceById(int id)
		{
			var ilce = _ilceInterface.GetAllIlceById(id);
			return Ok(ilce);
		}
		[HttpGet("GetIlceByIlId")]
		public ActionResult<List<Ilce>> GetAllIlceByIlId(int id)
		{
			var ilce = _ilceInterface.GetAllIlceByIlId(id);
			return Ok(ilce);
		}
	}
}

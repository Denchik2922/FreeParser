using DBL.Controllers;
using DBL.DataAccess;
using DBL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeParser.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BurseController : ControllerBase
	{
		/// <summary>
		/// База данных
		/// </summary>
		private readonly DBController db;

		public BurseController(DBContext context)
		{
			db = new DBController(context);
		}

		[HttpGet]
		public JsonResult Get()
		{
			List<Burse> burses = db.GetAll<Burse>().Select(b => new Burse()
			{
				Id = b.Id,
				Name = b.Name
			}).ToList();
			
			return new JsonResult(burses);
		}

		[HttpPost]
		public IActionResult Post(Burse burse)
		{
			var name = burse.Name;
			return Ok("Запрос принят");
		}

	}
}

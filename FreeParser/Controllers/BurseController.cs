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
		public IActionResult Get()
		{
			List<Category> burse = db.GetId<Burse>(5).Categories as List<Category>;


			return new OkObjectResult(burse);
		}

	}
}

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
	public class ExtraCategoryController : ControllerBase
	{
		/// <summary>
		/// База данных
		/// </summary>
		private readonly DBController db;

		public ExtraCategoryController(DBContext context)
		{
			db = new DBController(context);
		}

		[HttpGet]
		public JsonResult Get()
		{
			List<ExtraCategory> categories = db.GetAll<ExtraCategory>().Select(c => new ExtraCategory()
			{
				Id = c.Id,
				Name = c.Name,
				CategoryId = c.CategoryId

			}).ToList();

			return new JsonResult(categories);
		}
	}
}

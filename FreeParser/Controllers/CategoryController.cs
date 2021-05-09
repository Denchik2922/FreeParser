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
	public class CategoryController : ControllerBase
	{
		/// <summary>
		/// База данных
		/// </summary>
		private readonly DBController db;

		public CategoryController(DBContext context)
		{
			db = new DBController(context);
		}

		[HttpGet]
		public JsonResult Get()
		{
			List<Category> categories = db.GetAll<Category>().Select(c => new Category()
			{
				Id = c.Id,
				Name = c.Name,
				BurseId = c.BurseId
				
			}).ToList();

			return new JsonResult(categories);
		}
	}
}

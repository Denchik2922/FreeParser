using DBL.Controllers;
using DBL.Models;
using Microsoft.AspNetCore.Mvc;
using DBL.DataAccess;
using System;
using Telegram.Bot;
using FreeParser.Models;

namespace FreeParser.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class HomeController : ControllerBase
	{
		private readonly DBController db;

		public HomeController(DBContext context)
		{
			db = new DBController(context);
		}

		[HttpGet]
		public IActionResult Get()
		{
			return Ok("Get result is nulll.");
		}


	}
}

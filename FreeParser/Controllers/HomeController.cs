using FreeParser.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;

namespace FreeParser.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class HomeController : ControllerBase
	{
		[HttpGet]
		public async Task<IActionResult> Get()
		{
			TelegramBotClient client = await Bot.Get();


			var info =  await client.GetWebhookInfoAsync();
			return Ok(info);
		}


	}
}

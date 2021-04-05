using DBL.Controllers;
using DBL.DataAccess;
using FreeParser.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace FreeParser.Controllers
{
	[Route("api/[controller]/update")]
	[ApiController]
	public class MessageController : ControllerBase
	{
		/// <summary>
		/// База данных
		/// </summary>
		private readonly DBController db;

		public MessageController(DBContext context)
		{
			db = new DBController(context);
		}

		[HttpGet]
		public OkResult Get()
		{
			return Ok();
		}

		[HttpPost]
		public async Task<OkResult> Update([FromBody] Update update)
		{
			if (update == null) return Ok();

			var commands = Bot.Commands;
			var message = update.Message;
			var client = await Bot.Get();


			foreach (var command in commands)
			{
				if (command.Contains(message.Text))
				{
					await command.Execute(message, client, db);
					break;
				}
			}
			return Ok();
		}
	}
}

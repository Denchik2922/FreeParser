using DBL.Controllers;
using DBL.DataAccess;
using FreeParser.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

		private  ILogger<MessageController> _logger;

		public MessageController(DBContext context, ILogger<MessageController> logger)
		{
			_logger = logger;
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

			Message message;

			var commands = Bot.Commands;
			if (update.Message != null)
			{
				message = update.Message;
			}
			else if(update.CallbackQuery.Message != null)
			{
				message = update.CallbackQuery.Message;
			}
			else return Ok();

			var client = await Bot.Get();


			foreach (var command in commands)
			{
				try
				{
					if (command.Contains(message.Text))
					{
						await command.Execute(message, client, db);
						break;
					}
				}
				catch (Exception e)
				{
					_logger.LogError(e.Message);
				}
			}
			return Ok();
		}
	}
}

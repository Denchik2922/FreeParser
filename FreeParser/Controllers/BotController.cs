using FreeParser.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace FreeParser.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BotController : ControllerBase
	{
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
					await command.Execute(message, client);
					break;
				}
			}
			return Ok();
		}
	}
}

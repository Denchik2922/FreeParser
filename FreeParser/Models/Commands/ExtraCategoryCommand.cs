using DBL.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FreeParser.Models.Commands
{
	public class ExtraCategoryCommand : BaseCommand
	{
		public override string Name => "extraCategory:";

		public override async Task Execute(Message message, TelegramBotClient client, DBController db)
		{
			await client.DeleteMessageAsync(message.Chat.Id, message.MessageId);
		}
	}
}

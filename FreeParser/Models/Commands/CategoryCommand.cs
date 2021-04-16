using DBL.Controllers;
using DBL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FreeParser.Models.Commands
{
	public class CategoryCommand : BaseCommand
	{
		public override string Name => "/category";

		public override async Task Execute(Message message, TelegramBotClient client, DBController db)
		{
			var chatId = (int)message.Chat.Id;

			var categories = db.GetAll<Burse>();

			string cat = "";

			foreach(var c in categories)
			{
				cat += $"{c} \n";
			}

			await client.SendTextMessageAsync(chatId, cat);
		}
	}
}

using DBL.Controllers;
using DBL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace FreeParser.Models.Commands
{
	public class BurseCommand : BaseCommand
	{
		public override string Name => "burse:";

		public override async Task Execute(Message message, TelegramBotClient client, DBController db)
		{
			var chatId = (int)message.Chat.Id;
			int massageId = message.MessageId;

			int	idBurse = Convert.ToInt32(message.Text.Split(':')[1]);

			string messageForUser = "Выберите основную категорию: ";

			await client.EditMessageTextAsync(chatId, massageId, messageForUser, replyMarkup: (InlineKeyboardMarkup)GetButtons(idBurse, db));
		}

		private IReplyMarkup GetButtons(int idBurse, DBController db)
		{
			var keys = new List<InlineKeyboardButton>();
			var keyboard = new List<List<InlineKeyboardButton>>();

			List<Category> categories = db.GetAll<Category>().Where(c => c.Burse.Id == idBurse).ToList();

			foreach (var c in categories)
			{
				keyboard.Add(new List<InlineKeyboardButton>() { new InlineKeyboardButton { Text = c.Name, CallbackData = $"category:{c.Id}" } });
			}

			return new InlineKeyboardMarkup(keyboard);
		}
	}
}

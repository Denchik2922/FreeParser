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
	public class AllBurseCommand : BaseCommand
	{
		public override string Name => @"/all_burse";

		public override async Task Execute(Message message, TelegramBotClient client, DBController db, string callBackMessage)
		{
			var chatId = (int)message.Chat.Id;
			int messageId = message.MessageId;
			var burses = await db.GetAllAsync<Burse>();

			string messageForUser = "Выберите биржу";

			await client.EditMessageTextAsync(chatId, messageId, messageForUser, replyMarkup: (InlineKeyboardMarkup)GetButtons(burses));
		}

		private IReplyMarkup GetButtons(ICollection<Burse> burses)
		{
			var keyboard = new List<List<InlineKeyboardButton>>();
			
			foreach (var b in burses)
			{
				keyboard.Add(new List<InlineKeyboardButton>() { new InlineKeyboardButton { Text = b.Name, CallbackData = $"burse:{b.Id}" } });
			}

			keyboard.Add(new List<InlineKeyboardButton>() { new InlineKeyboardButton { Text = "🏠 Главное меню", CallbackData = $"/start" } });
			return new InlineKeyboardMarkup(keyboard);
		}
	}
}

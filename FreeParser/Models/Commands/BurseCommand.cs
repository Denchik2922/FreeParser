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

		public override async Task Execute(Message message, TelegramBotClient client, DBController db, string callBackMessage)
		{
			var chatId = (int)message.Chat.Id;
			int massageId = message.MessageId;

			int	idBurse = Convert.ToInt32(callBackMessage.Split(':')[1]);
			List<Category> allCategories = await db.GetAllAsync<Category>();
			List<Category> categories = allCategories.Where(c => c.Burse.Id == idBurse).ToList();

			string messageForUser = "Выберите основную категорию: ";

			await client.EditMessageTextAsync(chatId, massageId, messageForUser, replyMarkup: (InlineKeyboardMarkup)GetButtons(categories));
		}

		private IReplyMarkup GetButtons(List<Category> categories)
		{
			var keyboard = new List<List<InlineKeyboardButton>>();

			foreach (var c in categories)
			{
				keyboard.Add(new List<InlineKeyboardButton>() { new InlineKeyboardButton { Text = c.Name, CallbackData = $"category:{c.Id}" } });
			}
			keyboard.Add(new List<InlineKeyboardButton>() { new InlineKeyboardButton { Text = "Назад", CallbackData = $"/all_burse" } });
			keyboard.Add(new List<InlineKeyboardButton>() { new InlineKeyboardButton { Text = "Главное меню", CallbackData = $"/start" } });
			return new InlineKeyboardMarkup(keyboard);
		}
	}
}

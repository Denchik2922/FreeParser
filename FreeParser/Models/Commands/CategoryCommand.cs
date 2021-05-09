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
	public class CategoryCommand : BaseCommand
	{
		public override string Name => "category:";

		public override async Task Execute(Message message, TelegramBotClient client, DBController db, string callBackMessage)
		{
			var chatId = (int)message.Chat.Id;
			var messageId = message.MessageId;

			string messageForUser = "Выберите категорию: ";
			int idCategory = Convert.ToInt32(callBackMessage.Split(':')[1]);
			List<ExtraCategory> allExtraCategories = await db.GetAllAsync<ExtraCategory>();
			List<Category> allCategories = await db.GetAllAsync<Category>();
			var categories = allExtraCategories.Where(c => c.CategoryId == idCategory).ToList();
			int idBurse = (int)allCategories.First(c => c.Id == idCategory).BurseId;

			await client.EditMessageTextAsync(chatId, messageId, messageForUser, replyMarkup: (InlineKeyboardMarkup)GetButtons(idBurse, categories));
		}

		private IReplyMarkup GetButtons(int idBurse, List<ExtraCategory> categories)
		{
			var keyboard = new List<List<InlineKeyboardButton>>();
			
			foreach (var c in categories)
			{
				keyboard.Add(new List<InlineKeyboardButton>() { new InlineKeyboardButton { Text = c.Name, CallbackData = $"extraCategory:{c.Id}" } });
			}
			keyboard.Add(new List<InlineKeyboardButton>() { new InlineKeyboardButton { Text = "Назад", CallbackData = $"burse:{idBurse}" } });
			keyboard.Add(new List<InlineKeyboardButton>() { new InlineKeyboardButton { Text = "Главное меню", CallbackData = $"/start" } });

			return new InlineKeyboardMarkup(keyboard);
		}
	}
}

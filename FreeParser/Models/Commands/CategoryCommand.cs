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
			List<DBL.Models.User> allUsers = await db.GetAllAsync<DBL.Models.User>();

			var userExtraCategoies = allUsers.First(u => u.ClientId == chatId).ExtraCategories.ToList();
			var categories = allExtraCategories.Where(c => c.CategoryId == idCategory).ToList();
			int idBurse = (int)allCategories.First(c => c.Id == idCategory).BurseId;

			await client.EditMessageTextAsync(chatId, messageId, messageForUser, replyMarkup: (InlineKeyboardMarkup)GetButtons(idBurse, categories, userExtraCategoies));
		}

		private IReplyMarkup GetButtons(int idBurse, List<ExtraCategory> categories, List<ExtraCategory> userCategories)
		{
			var keyboard = new List<List<InlineKeyboardButton>>();
			
			foreach (var c in categories)
			{
				string textMessages = "";
				if (userCategories.Contains(c))
				{
					textMessages = $"✅ {c.Name}";
				}
				else
				{
					textMessages = c.Name;
				}


				keyboard.Add(new List<InlineKeyboardButton>() { new InlineKeyboardButton { Text = textMessages, CallbackData = $"extraCategory:{c.Id}" } });
			}
			keyboard.Add(new List<InlineKeyboardButton>() { new InlineKeyboardButton { Text = "⬅️ Назад", CallbackData = $"burse:{idBurse}" } });
			keyboard.Add(new List<InlineKeyboardButton>() { new InlineKeyboardButton { Text = "🏠 Главное меню", CallbackData = $"/start" } });

			return new InlineKeyboardMarkup(keyboard);
		}
	}
}

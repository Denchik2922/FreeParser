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
	public class ExtraCategoryCommand : BaseCommand
	{
		public override string Name => "extraCategory:";

		public override async Task Execute(Message message, TelegramBotClient client, DBController db, string callBackMessage)
		{
			var chatId = (int)message.Chat.Id;
			int messageId = message.MessageId;
			int idExtaCategory = Convert.ToInt32(callBackMessage.Split(':')[1]);

			List<DBL.Models.User> allUsers = await db.GetAllAsync<DBL.Models.User>();
			var user = allUsers.First(u => u.ClientId == chatId);

			List<ExtraCategory> extraCategories = await db.GetAllAsync<ExtraCategory>();
			var extraCategory = extraCategories.First(c => c.Id == idExtaCategory);

			AddCategoryForUser(user, db, extraCategory);

			string messageForUser = "Выберите категорию: ";

			int burseId = (int)extraCategory.Category.BurseId;
			List<ExtraCategory> concreteExtraCategories = extraCategories.Where(c => c.CategoryId == extraCategory.CategoryId).ToList();
			var userExtraCategoies = allUsers.First(u => u.ClientId == chatId).ExtraCategories.ToList();

			await client.EditMessageTextAsync(chatId, messageId, messageForUser, replyMarkup: (InlineKeyboardMarkup)GetButtons(burseId, concreteExtraCategories, userExtraCategoies));
			
		}

		private void AddCategoryForUser(DBL.Models.User user, DBController db, ExtraCategory extraCategory)
		{
				
			if (user.ExtraCategories.Contains(extraCategory))
			{
				
				user.ExtraCategories.Remove(extraCategory);
				db.Update<DBL.Models.User>(user);
			}
			else
			{
				user.ExtraCategories.Add(extraCategory);
				db.Update<DBL.Models.User>(user);
			}
			
		}

		private IReplyMarkup GetButtons(int burseId, List<ExtraCategory> categories, List<ExtraCategory> userCategories)
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
			keyboard.Add(new List<InlineKeyboardButton>() { new InlineKeyboardButton { Text = "⬅️ Назад", CallbackData = $"burse:{burseId}" } });
			keyboard.Add(new List<InlineKeyboardButton>() { new InlineKeyboardButton { Text = "🏠 Главное меню", CallbackData = $"/start" } });

			return new InlineKeyboardMarkup(keyboard);
		}
	}
}

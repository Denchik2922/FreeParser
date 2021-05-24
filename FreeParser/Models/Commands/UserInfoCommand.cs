using DBL.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace FreeParser.Models.Commands
{
	public class UserInfoCommand : BaseCommand
	{
		public override string Name => @"/user_info";

		public override async Task Execute(Message message, TelegramBotClient client, DBController db, string callBackMessage)
		{
			var chatId = (int)message.Chat.Id;
			int messageId = message.MessageId;

			string messageForUser = await GetUserInfo(db, chatId);

			await client.EditMessageTextAsync(chatId, messageId, messageForUser, replyMarkup: (InlineKeyboardMarkup)GetButtons());
		}

		private async Task<string> GetUserInfo(DBController db, int chatId)
		{
			var users = await db.GetAllAsync<DBL.Models.User>();
			var user = users.First(u => u.ClientId == chatId);
			string StatusParsing = user.IsActiveSendOrder ? "Активен" : "Не активен";

			return $"Статус рассылки: {StatusParsing};\n" +
				   $"Подписка на категории: {String.Join("", user.ExtraCategories)}";
		}

		private IReplyMarkup GetButtons()
		{
			var keyboard = new List<List<InlineKeyboardButton>>();
			
			keyboard.Add(new List<InlineKeyboardButton>() { new InlineKeyboardButton { Text = "🏠 Главное меню", CallbackData = $"/start" } });
			return new InlineKeyboardMarkup(keyboard);
		}
	}
}

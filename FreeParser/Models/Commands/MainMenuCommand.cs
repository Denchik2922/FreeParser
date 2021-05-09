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
	public class MainMenuCommand : BaseCommand
	{
		public override string Name => @"/start";

		public override async Task Execute(Message message, TelegramBotClient client, DBController db, string callBackMessage)
		{
			var chatId = (int)message.Chat.Id;
			int messageId = message.MessageId;

			try
			{
				await AddNewUser(db, chatId, message);
			}
			catch(Exception e)
			{
				throw new Exception($"Ошибка добавления пользователя \n Код ошибки {e.Message}");
			}
			

			string messageForUser = "Главное меню:";

			if (callBackMessage != null && callBackMessage.Length > 1)
			{
				await client.EditMessageTextAsync(chatId, messageId, messageForUser, replyMarkup: (InlineKeyboardMarkup)GetButtons());
			}
			else
			{
				await client.SendTextMessageAsync(chatId, messageForUser, replyMarkup: (InlineKeyboardMarkup)GetButtons());
			}
		}


		private async Task AddNewUser(DBController db, int chatId, Message message )
		{
			var users = await db.GetAllAsync<DBL.Models.User>();

			bool IsUserExist = users.Any(u => u.ClientId == chatId);

			if (!IsUserExist)
			{
				var user = new DBL.Models.User() { ClientId = chatId, FullName = message.Chat.Username };
				await db.AddAsync<DBL.Models.User>(user);
			}
		}

		private IReplyMarkup GetButtons()
		{
			
			var keyboard = new List<List<InlineKeyboardButton>>();

			keyboard.Add(new List<InlineKeyboardButton>() { new InlineKeyboardButton { Text = "Выбрать категории", CallbackData = $"/all_burse" } });
			keyboard.Add(new List<InlineKeyboardButton>() { new InlineKeyboardButton { Text = "Информация о пользователе", CallbackData = $"/user_info" } });
			keyboard.Add(new List<InlineKeyboardButton>() { new InlineKeyboardButton { Text = "Рассылка", CallbackData = $"/start_parsing" } });

			return new InlineKeyboardMarkup(keyboard);
		}
	}
}

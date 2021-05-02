using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using DBL.Controllers;
using System;
using Telegram.Bot.Types.ReplyMarkups;
using System.Collections.Generic;
using System.Linq;
using DBL.Models;

namespace FreeParser.Models.Commands
{
	public class StartCommand : BaseCommand
	{

		public override string Name => @"/start";

		public override async Task Execute(Message message, TelegramBotClient client, DBController db)
		{
			var chatId = (int)message.Chat.Id;

			string messageForUser = "Выберите биржу";

			var users = await db.GetAllAsync<DBL.Models.User>();

			bool IsUserExist = users.Any(u => u.ClientId == chatId);

			try
			{
				if (!IsUserExist)
				{
					var user = new DBL.Models.User() { ClientId = chatId, FullName = message.Chat.Username };
					await db.AddAsync<DBL.Models.User>(user);
					messageForUser = "Вы у нас впервые, для начала выберите биржу";
				}
			}
			catch (ArgumentException e)
			{
				throw new ArgumentException($"При добавленни нового пользователя возникла ошибка \nКод ошибки: {e.Message}");
			}
			finally
			{
				await client.SendTextMessageAsync(chatId, messageForUser, replyMarkup: GetButtons(db));
			}
		}



		private IReplyMarkup GetButtons(DBController db)
		{
			var keys = new List<InlineKeyboardButton>();
			foreach (var b in db.GetAll<Burse>())
			{
				keys.Add(new InlineKeyboardButton { Text = b.Name, CallbackData = $"burse:{b.Id}"});
			}

			var keyboard = new List<List<InlineKeyboardButton>>() {keys};
			return new InlineKeyboardMarkup(keyboard);
		}
	}
}

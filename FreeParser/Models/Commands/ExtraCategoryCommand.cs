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
			int chatId = (int)message.Chat.Id;
			int messageId = message.MessageId;
			
			int idExtaCategory = Convert.ToInt32(callBackMessage.Split(':')[1]);

			string messageForUser = "";

			try
			{
				messageForUser = await AddCategoryForUser(chatId, db, idExtaCategory);
			}
			catch(Exception e)
			{
				throw new Exception($"Ошибка при добавлении категории Код ошибки:{e.Message}");
			}


			try
			{
				await client.EditMessageTextAsync(chatId, messageId, messageForUser);
			}
			catch (Exception e)
			{
				throw new Exception($"Ошибка при отправки сообшения Код ошибки:{e.Message}");
			}
			


		}

		private async Task<string> AddCategoryForUser(int chatId, DBController db, int idExtaCategory)
		{
			ICollection<DBL.Models.User> allUsers = await db.GetAllAsync<DBL.Models.User>();
			var user = allUsers.First(u => u.ClientId == chatId);

			
			ICollection<ExtraCategory> extraCategories = await db.GetAllAsync<ExtraCategory>();
			var extraCategory = extraCategories.First(c => c.Id == idExtaCategory);

			
			if (user.ExtraCategories.Contains(extraCategory))
			{
				
				user.ExtraCategories.Remove(extraCategory);
				db.Update<DBL.Models.User>(user);
				return $"Вы отписались от категории {extraCategory.Name}";
			}
			else
			{
				user.ExtraCategories.Add(extraCategory);
				db.Update<DBL.Models.User>(user);
				return $"Вы подписались на категорию {extraCategory.Name}";
			}
			

		}
	}
}

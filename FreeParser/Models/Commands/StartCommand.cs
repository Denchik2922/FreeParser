using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using DBL.Controllers;
using System;

namespace FreeParser.Models.Commands
{
	public class StartCommand : BaseCommand
	{

		public override string Name => @"/start";

		public override async Task Execute(Message message, TelegramBotClient client, DBController db)
		{
			var chatId = (int)message.Chat.Id;
			var messageId = message.MessageId;


			var user = new DBL.Models.User(){ ClientId = chatId, FullName = message.Chat.Username };
			string usersCount = "";

			try
			{
				db.Add<DBL.Models.User>(user);
			}
			catch (ArgumentException e)
			{

			}

			var users = db.GetAll<DBL.Models.User>();
			foreach (var us in users)
			{
				usersCount += us.ToString() + "\n";
			}



			await client.SendTextMessageAsync(chatId, usersCount, replyToMessageId: messageId);
		}
	}
}

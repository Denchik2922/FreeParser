using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FreeParser.Models.Commands
{
	public class StartCommand : BaseCommand
	{
		/*public UserController db = new UserController();*/

		public override string Name => @"/start";

		public override async Task Execute(Message message, TelegramBotClient client)
		{
			var chatId = (int)message.Chat.Id;
			var messageId = message.MessageId;



			/*var user = new Bl.Models.User() { ClientId = chatId, FullName = message.Chat.Username };
			string usersCount = "";

			try
			{
				db.Add<Bl.Models.User>(user);
			}
			catch (ArgumentException e)
			{

			}

			var users = db.GetAll<Bl.Models.User>();
			foreach (var us in users)
			{
				usersCount += us.ToString() + "\n";
			}*/



			await client.SendTextMessageAsync(chatId, "Hi", replyToMessageId: messageId);
		}
	}
}

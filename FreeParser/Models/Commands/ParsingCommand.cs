using DBL.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FreeParser.Models.Commands
{
	public class ParsingCommand : BaseCommand
	{
		public override string Name => @"/start_parsing";

		public override async Task Execute(Message message, TelegramBotClient client, DBController db, string callBackMessage = null)
		{
			var chatId = (int)message.Chat.Id;

			string messageForUser = await ChangeIsSendOrder(db, chatId);

			await client.SendTextMessageAsync(chatId, messageForUser);
		}

		private async Task<string> ChangeIsSendOrder(DBController db, int chatId)
		{
			var users = await db.GetAllAsync<DBL.Models.User>();
			var user = users.First(u => u.ClientId == chatId);
			string mes = "";

			if(user.IsActiveSendOrder == false)
			{
				user.IsActiveSendOrder = true;
				mes = "Теперь вы будете получать уведомление с бирж!";
			}
			else
			{
				user.IsActiveSendOrder = false;
				mes = "Вы отменили рассылку!";
			}
			db.Update<DBL.Models.User>(user);

			return mes;
		}
	}
}

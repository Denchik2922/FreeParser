using FreeParser.Models.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;

namespace FreeParser.Models
{
	public static class Bot
	{
		private static TelegramBotClient client;

		private static List<BaseCommand> commandsList;

		public static IReadOnlyList<BaseCommand> Commands { get => commandsList.AsReadOnly(); }

		public static async Task<TelegramBotClient> Get()
		{
			if (client != null)
			{
				return client;
			}

			commandsList = new List<BaseCommand>();
			commandsList.Add(new StartCommand());


			client = new TelegramBotClient(BotSettings.Key);
			var hook = string.Format(BotSettings.Url, "api/bot");
			await client.SetWebhookAsync(hook);

			return client;
		}
	}
}

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
		/// <summary>
		/// Клиент телеграма.
		/// </summary>
		private static TelegramBotClient client;

		/// <summary>
		/// Список команд.
		/// </summary>
		private static List<BaseCommand> commandsList;

		/// <summary>
		/// Команды
		/// </summary>
		public static IReadOnlyList<BaseCommand> Commands { get => commandsList.AsReadOnly(); }

		public static async Task<TelegramBotClient> Get()
		{
			if (client != null)
			{
				return client;
			}

			commandsList = new List<BaseCommand>();
			commandsList.Add(new StartCommand());
			commandsList.Add(new CategoryCommand());
			commandsList.Add(new BurseCommand());
			commandsList.Add(new ExtraCategoryCommand());

			client = new TelegramBotClient(BotSettings.Key);
			var hook = string.Format(BotSettings.Url, "api/Message/Update");
			await client.SetWebhookAsync(hook);

			return client;
		}
	}
}

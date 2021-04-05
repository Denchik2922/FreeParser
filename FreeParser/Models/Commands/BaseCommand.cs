using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using DBL.Controllers;

namespace FreeParser.Models.Commands
{
	public abstract class BaseCommand
	{

		/// <summary>
		/// Название команды
		/// </summary>
		public abstract string Name { get; }

		/// <summary>
		/// Выполнение команды.
		/// </summary>
		/// <param name="message"> Сообщение от бота. </param>
		/// <param name="client"> Бот.</param>
		public abstract Task Execute(Message message, TelegramBotClient client, DBController db);

		/// <summary>
		/// Сравнение команды.
		/// </summary>
		/// <param name="command"> Название команды. </param>
		/// <returns> bool </returns>
		public bool Contains(string command)
		{
			return command.Contains(this.Name);
		}
	}
}

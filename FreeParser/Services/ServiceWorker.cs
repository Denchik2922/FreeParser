using Microsoft.Extensions.Logging;
using DBL.DataAccess;
using DBL.Controllers;
using Telegram.Bot;
using FreeParser.Models;
using System.Threading.Tasks;
using DBL.Models;

namespace FreeParser.Services
{
	public class ServiceWorker : IServiceWorker
	{
		private readonly ILogger<ServiceWorker> logger;

		private TelegramBotClient botClient;

		public ServiceWorker(ILogger<ServiceWorker> logger)
		{
			this.logger = logger;
			
			
		}

		public async Task DoWork( DBController db)
		{
			var user = db.GetId<User>(3);
			botClient = await Bot.Get();
			await botClient.SendTextMessageAsync(user.ClientId, $"Парсер для {user.FullName}"); ;
			logger.LogInformation("Parsing working");
		}
	}
}

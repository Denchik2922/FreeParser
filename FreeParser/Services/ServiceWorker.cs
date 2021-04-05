using Microsoft.Extensions.Logging;
using DBL.DataAccess;
using DBL.Controllers;
using Telegram.Bot;
using FreeParser.Models;

namespace FreeParser.Services
{
	public class ServiceWorker : IServiceWorker
	{
		private readonly ILogger<ServiceWorker> logger;

		private readonly DBController db;

		private readonly TelegramBotClient botClient;

		public ServiceWorker(ILogger<ServiceWorker> logger, DBContext context)
		{
			this.logger = logger;
			db = new DBController(context);
			
		}

		public void DoWork()
		{
			
			logger.LogInformation("Parsing working");
		}
	}
}

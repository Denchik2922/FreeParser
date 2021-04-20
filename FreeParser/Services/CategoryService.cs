using FreeParser.Services.ServiceSettings;
using FreeParser.Services.Workers;
using Microsoft.Extensions.Logging;

namespace FreeParser.Services
{
	public class CategoryService :BaseHostedService
	{
		private ICategoryWorker _serviceWorker;

		public CategoryService(ILogger<CategoryService> logger, ICategoryWorker serviceWorker)
		{
			_logger = logger;
			_serviceWorker = serviceWorker;
			_parsingPeriod = CategoryServiceSetting.ParsingPeriod;
			_nameService = "Парсинг категорий";
		}

		public override void DoWork(object state)
		{
			_serviceWorker.DoWork();
		}
	}
}

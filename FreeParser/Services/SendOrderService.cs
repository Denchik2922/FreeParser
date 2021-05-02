using FreeParser.Services.ServiceSettings;
using FreeParser.Services.Workers;
using Microsoft.Extensions.Logging;

namespace FreeParser.Services
{
	public class SendOrderService : BaseHostedService
	{
		private ISendOrderWorker _serviceWorker;

		public SendOrderService(ILogger<SendOrderService> logger, ISendOrderWorker serviceWorker)
		{
			_logger = logger;
			_serviceWorker = serviceWorker;
			_parsingPeriod = SendOrderSettings.ParsingPeriod;
			_nameService = "Рассылка заказов";
		}

		public override void DoWork(object state)
		{
			/*_serviceWorker.DoWork();*/
		}
	}
}

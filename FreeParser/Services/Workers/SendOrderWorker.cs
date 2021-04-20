using DBL.Models;
using FreeParser.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;

namespace FreeParser.Services.Workers
{
	public class SendOrderWorker : BaseWorker, ISendOrderWorker
	{
		private TelegramBotClient botClient;

		public SendOrderWorker(ILogger<SendOrderWorker> logger, IServiceProvider serviceProvider) : base(logger, serviceProvider)
		{
			parserWorker.OnNewOrder += Parser_OnNewOrder;
		}

		private async void Parser_OnNewOrder(object arg1, List<Order> arg2)
		{
			try
			{
				foreach (var item in arg2)
				{
					bool IsOrderExist = db.GetAll<Order>().Any(o => o.Url.Contains(item.Url));
					var extraCategory = new List<ExtraCategory>();

					if (!IsOrderExist)
					{
						await SendOrder(item);
						foreach (var c in item.ExtraCategories)
						{
							extraCategory.Add(db.GetAll<ExtraCategory>().Find(ec => ec.Name.Trim().ToLower().Contains(c.Name.Trim().ToLower())));
						}
						item.ExtraCategories = extraCategory;


						db.Add<Order>(item);
					}
				}
			}
			catch (Exception e)
			{
				logger.LogError(e.Message);
			}

		}

		public async Task SendOrder(Order order)
		{
			await botClient.SendTextMessageAsync(394143523, order.ToString());
		}

		public override async void DoWork()
		{
			botClient = await Bot.Get();

			try
			{
				await parserWorker.GetOrders();
				logger.LogInformation("Заказы успешно спарсились");
				
			}
			catch (Exception e)
			{
				logger.LogError(e.Message);
			}




		}
	}
}

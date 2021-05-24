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
			parserWorker.OnNewOrderAsync += Parser_OnNewOrder;
		}

		private async Task Parser_OnNewOrder(List<Order> arg2)
		{
			try
			{
				foreach (var item in arg2)
				{
					List<Order> orders = await db.GetAllAsync<Order>();
					bool IsOrderExist = orders.Any(o => o.Url.Contains(item.Url));
					var extraCategory = new List<ExtraCategory>();

					if (!IsOrderExist)
					{
						await SendOrder(item);
						foreach (var c in item.ExtraCategories)
						{
							try
							{
								List<ExtraCategory> extraCategories = await db.GetAllAsync<ExtraCategory>();
								ExtraCategory extraCat = extraCategories.FirstOrDefault(ec => ec.Name.Trim().ToLower().Contains(c.Name.Trim().ToLower()));
								if(extraCat != null)
								{
									extraCategory.Add(extraCat);
								}
							}
							catch(Exception e)
							{
								logger.LogError(e.Message);
							}
							
						}
						item.ExtraCategories = extraCategory;

						await db.AddAsync<Order>(item);
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
			var users = await db.GetAllAsync<User>();
			foreach(var user in users)
			{
				if (user.IsActiveSendOrder)
				{
					foreach(var orderCat in order.ExtraCategories)
					{
						bool userExistCategory = user.ExtraCategories.ToList().Exists(c => c.Name.Contains(orderCat.Name));
						if (userExistCategory)
						{
							try
							{
								await botClient.SendTextMessageAsync(user.ClientId, order.ToString());
								break;
							}
							catch(Exception e)
							{
								logger.LogError($"Ошибка при оправке заказов\nКод ошибки:{e.Message}");
							}
							
						}
					}
				}
			}
		}

		public override async Task DoWork()
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

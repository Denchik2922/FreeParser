using Microsoft.Extensions.Logging;
using DBL.DataAccess;
using DBL.Controllers;
using Telegram.Bot;
using FreeParser.Models;
using System.Threading.Tasks;
using DBL.Models;
using BurseParse.Burses.Freelancehunt;
using BurseParse.Burses.Kabanchik;
using BurseParse.Core;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace FreeParser.Services
{
	public class ServiceWorker : IServiceWorker
	{
		private readonly ILogger<ServiceWorker> logger;

		private readonly ParserWorker parserWorker;

		private Dictionary<IParser, IParserSettings> parsers;

		private TelegramBotClient botClient;

		private DBController db;

		public ServiceWorker(ILogger<ServiceWorker> logger,IServiceProvider serviceProvider)
		{
			this.logger = logger;
			var context = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<DBContext>();
			db = new DBController(context);

			parsers = new Dictionary<IParser, IParserSettings>();
			parsers.Add(new FreelancehuntParser(), new FrelancehuntSettings(2, 3));
			parsers.Add(new KabanchikParser(), new KabanchikSettings(2, 3));

			parserWorker = new ParserWorker();
			parserWorker.OnNewData += Parser_OnNewData;
			parserWorker.OnNewCategory += Parser_OnNewCategory;

			parserWorker.AddParsers(parsers);
		}

		public void DoWork()
		{
			foreach (var parser in parsers)
			{
				try
				{
					var IsBurseExist = db.GetAll<Burse>().Any(b => b.Name == parser.Value.BurseName);
					if (!IsBurseExist)
					{
						db.Add<Burse>(new Burse() { Name = parser.Value.BurseName });
					}	
				}
				catch (Exception e) 
				{
					logger.LogError(e.Message);
				}
			}

			parserWorker.GetCategories();


			logger.LogInformation("Parsing working");
		}


		private void Parser_OnNewCategory(object arg1, Dictionary<string, List<Category>> arg2)
		{
			foreach (var item in arg2)
			{
				try
				{
					var burse = db.GetAll<Burse>().Find(b => b.Name == item.Key);

					foreach (var category in item.Value)
					{
						if (!burse.Categories.Any(c => c.Name.Contains(category.Name)))
						{
							logger.LogInformation($"{!burse.Categories.Any(c => c.Name.Contains(category.Name))}");
							burse.Categories.Add(category);
						}
					}
					db.Update<Burse>(burse);
				}
				catch (Exception e)
				{
					logger.LogError(e.Message);
				}
			}
		}

		private void Parser_OnNewData(object arg1, List<Order> arg2)
		{
			throw new NotImplementedException();
		}
	}
}

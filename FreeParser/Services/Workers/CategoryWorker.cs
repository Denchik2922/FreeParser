using Microsoft.Extensions.Logging;
using DBL.DataAccess;
using DBL.Controllers;
using DBL.Models;
using BurseParse.Burses.Freelancehunt;
using BurseParse.Core;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace FreeParser.Services.Workers
{
	public class CategoryWorker : BaseWorker, ICategoryWorker
	{
		public CategoryWorker(ILogger<CategoryWorker> logger, IServiceProvider serviceProvider) : base(logger, serviceProvider)
		{
			parserWorker.OnNewCategory += Parser_OnNewCategory;			
		}

		public override async void DoWork()
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

			try
			{
				await parserWorker.GetCategories();
				logger.LogInformation("Категории спарсились успешно");
			}
			catch (Exception e)
			{
				logger.LogError(e.Message);
			}



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

		
	}
}

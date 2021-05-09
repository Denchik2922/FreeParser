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
using System.Threading.Tasks;

namespace FreeParser.Services.Workers
{
	public class CategoryWorker : BaseWorker, ICategoryWorker
	{
		public CategoryWorker(ILogger<CategoryWorker> logger, IServiceProvider serviceProvider) : base(logger, serviceProvider)
		{
			parserWorker.OnNewCategory += Parser_OnNewCategory;			
		}

		public override async Task DoWork()
		{
			foreach (var parser in parsers)
			{
				var burses = new List<Burse>();
				try
				{
					 burses = await db.GetAllAsync<Burse>();
				}
				catch (Exception e)
				{
					logger.LogError(e.Message);
				}

				var IsBurseExist = burses.Any(b => b.Name == parser.Value.BurseName);
				if (!IsBurseExist)
				{
					await db.AddAsync<Burse>(new Burse() { Name = parser.Value.BurseName });
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
				bool IsNew = false;
				try
				{
					var burse = db.GetAll<Burse>().Find(b => b.Name == item.Key);

					foreach (var category in item.Value)
					{
						if (!burse.Categories.Any(c => c.Name.Trim().ToLower().Contains(category.Name.Trim().ToLower())))
						{

							logger.LogInformation($"{!burse.Categories.Any(c => c.Name.Contains(category.Name))}  :{category.Name.Trim().ToLower()}:");
							burse.Categories.Add(category);
							IsNew = true;
						}
					}
					if (IsNew)
					{
						db.Update<Burse>(burse);
					}
					
				}
				catch (Exception e)
				{
					logger.LogError(e.Message);
				}
			}
		}

		
	}
}

using AngleSharp.Html.Dom;
using BurseParse.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBL.Models;

namespace BurseParse.Burses.Freelancehunt
{
	public class FreelancehuntParser : IParser
	{
		public List<Category> ParseCategory(IHtmlDocument document)
		{
			var numCategories = new List<string>();

			var list = new List<Category>();

			var categories = document.QuerySelectorAll("a.title");
			
			//Получаем теги категорий.
			foreach (var category in categories)
			{
				numCategories.Add(category.GetAttribute("href"));
			}

			foreach(var num in numCategories)
			{
				string nameCategory = "";
				var extraCategory = new List<ExtraCategory>();

				var extraCategories = document.QuerySelectorAll($"ul#{num.Split('#')[1]} > li > a");

				//Получаем название дополнительных категорий.
				foreach(var ex in extraCategories)
				{
					extraCategory.Add(new ExtraCategory()
					{
						Name = ex.LastChild.TextContent
					});
				}

				//Получаем название основной категории.
				foreach (var title in categories)
				{
					if(title.GetAttribute("href") == num)
					{
						nameCategory = title.TextContent;
					}
				}

				list.Add(new Category()
				{
					Name = nameCategory,
					ExtraCategories = extraCategory
				});
	
			}
			return list;
		}

		public List<Order> ParseOrder(IHtmlDocument document)
		{
			var list = new List<Order>();
			

			var names = document.QuerySelectorAll("td.left > a");
			var categories = document.QuerySelectorAll("td.left > div > small");
			var times = document.QuerySelectorAll("div.with-tooltip > h2");

			var ordersInfo = names.Zip(categories, (n, c) => new { n, c }).Zip(times, (t, r) => new { Name = t.n, Category = t.c, time = r });

			foreach (var order in ordersInfo)
			{
				var time = order.time.TextContent.Split(':');
				var listCategories = new List<ExtraCategory>();

				foreach (var c in order.Category.TextContent.Split(','))
				{
					listCategories.Add(new ExtraCategory() { Name = c });
				}

				list.Add(new Order()
				{
					ExtraCategories = listCategories,
					Description = order.Name.TextContent,
					Url = order.Name.GetAttribute("href"),
					Date = DateTime.Now.Date.Add(new TimeSpan(Convert.ToInt32(time[0]), Convert.ToInt32(time[1]), 00)),
					

				});

			}

			return list;
		}
	}
}

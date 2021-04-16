﻿using AngleSharp.Html.Dom;
using BurseParse.Core;
using DBL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BurseParse.Burses.Kabanchik
{
	public class KabanchikParser : IParser
	{

		public List<Category> ParseCategory(IHtmlDocument document)
		{
			var list = new List<Category>();
			var categories = document.QuerySelectorAll("a.title");

			foreach (var category in categories)
			{
				list.Add(new Category
				{
					Name = category.TextContent
				});
			}
			return list;
		}

		public List<object> ParseExtraCategory(IHtmlDocument document)
		{
			var list = new List<object>();
			var categories = document.QuerySelectorAll("li.accordion-inner > a");

			foreach (var category in categories)
			{
				list.Add(new
				{
					Name = category.TextContent
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

				list.Add(new Order()
				{
					Description = order.Name.TextContent,
					Url = order.Name.GetAttribute("href"),
					Date = DateTime.Now.Date.Add(new TimeSpan(Convert.ToInt32(time[0]), Convert.ToInt32(time[1]), 00))
				});

			}


			return list;
		}
	}
}

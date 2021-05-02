﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DBL.Models
{
	public class Order : BaseModel
	{
		public Order()
		{
			ExtraCategories = new List<ExtraCategory>();
		}

		/// <summary>
		/// Описание.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Ссылка на заказ.
		/// </summary>
		public string Url { get; set; }

		/// <summary>
		/// Дата заказа.
		/// </summary>
		public DateTime Date { get; set; }

		/// <summary>
		/// Список дополнительных категорий.
		/// </summary>
		public virtual ICollection<ExtraCategory> ExtraCategories { get; set; }

		public override string ToString()
		{
			return $"{Description} \n" +
				$"{Url} \n" +
				$"{string.Join(",", ExtraCategories)}";
		}
	}
}

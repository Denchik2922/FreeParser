using System;
using System.Collections.Generic;
using System.Text;

namespace DBL.Models
{
	public class ExtraCategory : BaseModel
	{
		public ExtraCategory()
		{
			Orders = new List<Order>();
			Users = new List<User>();
		}

		/// <summary>
		/// Название дополнителоной категории.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Id главной категории.
		/// </summary>
		public int? CategoryId { get; set; }

		/// <summary>
		/// Главная категория.
		/// </summary>
		public virtual Category Category { get; set; }

		/// <summary>
		/// Заказы.
		/// </summary>
		public virtual ICollection<Order> Orders { get; set; }

		/// <summary>
		/// Пользователь категории.
		/// </summary>
		public virtual ICollection<User> Users { get; set; }


		public override string ToString()
		{
			return $"{Name}";
		}
	}
}

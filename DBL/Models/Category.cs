using System;
using System.Collections.Generic;
using System.Text;

namespace DBL.Models
{
	public class Category : BaseModel
	{
		/// <summary>
		/// Название категории.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Id биржи
		/// </summary>
		public int BurseId { get; set; }

		/// <summary>
		/// Биржа.
		/// </summary>
		public Burse Burse { get; set; }

		/// <summary>
		/// Список дополнительных категорий.
		/// </summary>
		public ICollection<ExtraCategory> ExtraCategories { get; set; }

		public override string ToString()
		{
			return $"{Name}";
		}

	}
}

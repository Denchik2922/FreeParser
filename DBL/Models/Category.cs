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
		public int? BurseId { get; set; }

		/// <summary>
		/// Биржа.
		/// </summary>
		public virtual Burse Burse { get; set; }

		/// <summary>
		/// Список дополнительных категорий.
		/// </summary>
		public virtual ICollection<ExtraCategory> ExtraCategories { get; set; }

		public override string ToString()
		{
			return $"{Name}";
		}

	}
}

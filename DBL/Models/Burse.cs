using System;
using System.Collections.Generic;
using System.Text;

namespace DBL.Models
{
	public class Burse : BaseModel
	{

		/// <summary>
		/// Название биржи.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Список категорий.
		/// </summary>
		public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

		public override string ToString()
		{
			return $"Name Burse:{Name}; \n Categories: {Categories.Count};";
		}
	}
}

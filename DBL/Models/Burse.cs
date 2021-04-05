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
		public ICollection<Category> Categories { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Text;

namespace DBL.Models
{
	/// <summary>
	/// Пользователь.
	/// </summary>
	public class User : BaseModel
	{
		public User()
		{
			ExtraCategories = new List<ExtraCategory>();
		}

		/// <summary>
		/// Id телеграм клиента.
		/// </summary>
		public int ClientId { get; set; }

		/// <summary>
		/// Username пользователя. 
		/// </summary>
		public string FullName { get; set; }

		/// <summary>
		/// Подписка.
		/// </summary>
		public Subscribe Subscribe { get; set; }

		/// <summary>
		/// Список категорий пользователя.
		/// </summary>
		public ICollection<ExtraCategory> ExtraCategories { get; set; }

		/// <summary>
		/// Права доступа.
		/// </summary>
		public Permission Permission { get; set; }

		public override string ToString()
		{
			return $"ClientID {ClientId} Name {FullName}";
		}
	}
}

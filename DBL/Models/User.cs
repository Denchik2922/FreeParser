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
		/// Уведомление с бирж. 
		/// </summary>
		public bool IsActiveSendOrder { get; set; } = false;

		/// <summary>
		/// Подписка.
		/// </summary>
		public virtual Subscribe Subscribe { get; set; }

		/// <summary>
		/// Список категорий пользователя.
		/// </summary>
		public virtual ICollection<ExtraCategory> ExtraCategories { get; set; }

		/// <summary>
		/// Права доступа.
		/// </summary>
		public virtual Permission Permission { get; set; }

		public override string ToString()
		{
			return $"ClientID {ClientId} Name {FullName}";
		}
	}
}

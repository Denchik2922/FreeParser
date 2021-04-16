using System;
using System.Collections.Generic;
using System.Text;

namespace DBL.Models
{
	public class Subscribe : BaseModel
	{
		/// <summary>
		/// Статус подписки.
		/// </summary>
		public bool IsSubscribe { get; set; }

		/// <summary>
		/// Конец подписки.
		/// </summary>
		public DateTime EndSubscribe { get; set; }

		/// <summary>
		/// Id пользователя подписки.
		/// </summary>
		public int? UserId { get; set; }

		/// <summary>
		/// Пользователь подписки
		/// </summary>
		public virtual User User { get; set; }
	}
}

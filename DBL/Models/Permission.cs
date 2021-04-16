using System;
using System.Collections.Generic;
using System.Text;

namespace DBL.Models
{
	public class Permission : BaseModel
	{
		/// <summary>
		/// Пользователь админ.
		/// </summary>
		public bool IsAdmin { get; set; }

		/// <summary>
		/// Пользователь разработчик.
		/// </summary>
		public bool IsStaff { get; set; }

		/// <summary>
		/// Id gользователя.
		/// </summary>
		public int? UserId { get; set; }

		/// <summary>
		/// Пользователь прав.
		/// </summary>
		public virtual User User { get; set; }
	}
}

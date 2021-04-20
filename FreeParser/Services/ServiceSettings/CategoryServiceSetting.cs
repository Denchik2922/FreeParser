using System;

namespace FreeParser.Services.ServiceSettings
{
	public class CategoryServiceSetting
	{
		/// <summary>
		/// Пауза между парсингом категорий.
		/// </summary>
		public static readonly TimeSpan ParsingPeriod = TimeSpan.FromMinutes(50);
	}
}

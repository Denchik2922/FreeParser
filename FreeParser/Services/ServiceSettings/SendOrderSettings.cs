using System;

namespace FreeParser.Services.ServiceSettings
{
	public class SendOrderSettings
	{
		/// <summary>
		/// Пауза между парсингом заказов.
		/// </summary>
		public static readonly TimeSpan ParsingPeriod = TimeSpan.FromMinutes(10);
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeParser.Models
{
	public static class BotSettings
	{
		/// <summary>
		/// Адресс сервера.
		/// </summary>
		public static string Url { get; set; } = "https://free-parser.herokuapp.com:443/{0}";

		/// <summary>
		/// Название бота.
		/// </summary>
		public static string Name { get; set; } = "FreeParserAllFreelanceBirgBot";

		/// <summary>
		/// Токен бота.
		/// </summary>
		public static string Key { get; set; } = "1525823832:AAHLZPtzZT47MLZBZqoId2FkSrZP4utRGW8";

	}
}

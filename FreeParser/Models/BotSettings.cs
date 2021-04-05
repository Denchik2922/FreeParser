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
		public static string Url { get; set; } = "https://telegrambotfreeparser.azurewebsites.net:443/{0}";

		/// <summary>
		/// Название бота.
		/// </summary>
		public static string Name { get; set; } = "free_parser_burse_bot";

		/// <summary>
		/// Токен бота.
		/// </summary>
		public static string Key { get; set; } = "1738458660:AAGChjMOWACyUO-h72Ug_CCacLF4ygWSVfM";
	}
}

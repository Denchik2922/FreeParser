using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeParser.Services
{
	public static class ServiceSettings
	{
		/// <summary>
		/// Пауза между парсингом.
		/// </summary>
		public static readonly TimeSpan ParsingPeriod = TimeSpan.FromMinutes(50);
	}
}

using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeParser.Services
{
	public class ServiceWorker : IServiceWorker
	{
		private readonly ILogger<ServiceWorker> logger;

		public ServiceWorker(ILogger<ServiceWorker> logger)
		{
			this.logger = logger;
		}

		public void DoWork()
		{
			logger.LogInformation("I`m working");
		}
	}
}

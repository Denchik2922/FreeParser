using DBL.Controllers;
using DBL.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FreeParser.Services
{
	public class HostedService : IHostedService, IDisposable
	{
		private readonly ILogger<HostedService> logger;
		private Timer _timer;
		private bool IsRunning = false; 
		private IServiceWorker serviceWorker;
		private readonly IServiceScopeFactory scopeFactory;

		public HostedService(ILogger<HostedService> logger, IServiceWorker serviceWorker, IServiceProvider serviceProvider)
		{
			this.logger = logger;
			this.serviceWorker = serviceWorker;
		}

		public void Dispose()
		{
			_timer.Dispose();
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			if(IsRunning == false)
			{
				IsRunning = true;
				logger.LogInformation("RecureHostedService is Starting");
				_timer = new Timer(DoWork, null, TimeSpan.Zero, ServiceSettings.ParsingPeriod);
				return Task.FromResult("Парсинг успешно запущен!");
			}

			return Task.FromResult("Парсинг уже был запущен раньше!");
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			if(IsRunning == true)
			{
				IsRunning = false;
				logger.LogInformation("RecureHostedService is Stopping");
				_timer?.Change(Timeout.Infinite, 0);
				return Task.FromResult("Парсинг успешно остановлен!");
			}


			return Task.FromResult("Парсинг еще не запущен!");
		}

		private void DoWork(object state)
		{
			serviceWorker.DoWork();
		}
	}
}

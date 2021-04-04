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
		private IServiceWorker serviceWorker;

		public HostedService(ILogger<HostedService> logger, IServiceWorker serviceWorker)
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
			logger.LogInformation("RecureHostedService is Starting");
			_timer = new Timer(DoWork, null, TimeSpan.Zero, ServiceSettings.ParsingPeriod);
			return Task.CompletedTask;
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			logger.LogInformation("RecureHostedService is Stopping");
			_timer?.Change(Timeout.Infinite, 0);
			return Task.CompletedTask;
		}

		private void DoWork(object state)
		{
			serviceWorker.DoWork();
		}
	}
}

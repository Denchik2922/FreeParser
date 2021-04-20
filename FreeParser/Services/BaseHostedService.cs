using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FreeParser.Services
{
	public abstract class BaseHostedService : IHostedService, IDisposable
	{
		protected ILogger<BaseHostedService> _logger;
		protected Timer _timer;
		protected bool _isRunning = false;
		protected string _nameService;
		protected TimeSpan _parsingPeriod;

		public void Dispose()
		{
			_timer.Dispose();
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			if (_isRunning == false)
			{
				_isRunning = true;
				_logger.LogInformation($"{_nameService} успешно запущен!");
				_timer = new Timer(DoWork, null, TimeSpan.Zero, _parsingPeriod);
				return Task.FromResult($"{_nameService} успешно запущен!");
			}

			return Task.FromResult($"{_nameService} уже был запущен раньше!");
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			if (_isRunning == true)
			{
				_isRunning = false;
				_logger.LogInformation($"{_nameService} успешно остановлен!");
				_timer?.Change(Timeout.Infinite, 0);
				return Task.FromResult($"{_nameService} успешно остановлен!");
			}

			
			return Task.FromResult($"{_nameService} еще не запущен!");
		}

		public abstract void DoWork(object state);
	}
}

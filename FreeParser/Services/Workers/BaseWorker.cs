using BurseParse.Burses.Freelancehunt;
using BurseParse.Core;
using DBL.Controllers;
using DBL.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeParser.Services.Workers
{
	public abstract class BaseWorker
	{
		protected readonly ILogger<BaseWorker> logger;

		protected readonly ParserWorker parserWorker;

		protected Dictionary<IParser, IParserSettings> parsers;

		protected DBController db;

		public BaseWorker(ILogger<BaseWorker> logger, IServiceProvider serviceProvider)
		{
			this.logger = logger;
			var context = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<DBContext>();
			db = new DBController(context);

			parsers = new Dictionary<IParser, IParserSettings>();
			parsers.Add(new FreelancehuntParser(), new FrelancehuntSettings(2, 3));
			parserWorker = new ParserWorker();
			parserWorker.AddParsers(parsers);

			
		}

		public abstract Task DoWork();
	
	}
}

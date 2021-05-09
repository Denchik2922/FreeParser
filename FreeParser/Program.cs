using DBL.DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeParser
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().MigrateDatabase().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}

    public static class IWebHostExtensions
    {
        public static IHost MigrateDatabase(this IHost webHost)
        {
           
            var envAutoMigrate = Environment.GetEnvironmentVariable("AUTO_MIGRATE");
            if (envAutoMigrate != null && envAutoMigrate == "true")
            {
                Console.WriteLine("*** AUTO MIGRATE ***");

                var serviceScopeFactory = (IServiceScopeFactory)webHost.Services.GetService(typeof(IServiceScopeFactory));

                using (var scope = serviceScopeFactory.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    var dbContext = services.GetRequiredService<DBContext>();

                    dbContext.Database.Migrate();
                }
            }

            return webHost;
        }
    }
}

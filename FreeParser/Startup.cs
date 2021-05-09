using DBL.DataAccess;
using FreeParser.Models;
using FreeParser.Services;
using FreeParser.Services.Workers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace FreeParser
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			
			services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
			services.AddSingleton<ICategoryWorker, CategoryWorker>();
			services.AddSingleton<ISendOrderWorker, SendOrderWorker>();
			services.AddHostedService<CategoryService>();
			services.AddHostedService<SendOrderService>();


			var builder = new PostgreSqlConnectionStringBuilder("postgres://wdbmfvmjyylzym:5216095095117041e57c3d2106f05efca4409510455913bcc29c1931c9eafec2@ec2-34-254-120-2.eu-west-1.compute.amazonaws.com:5432/d1iniccdpra9a")
			{
				Pooling = true,
				TrustServerCertificate = true,
				SslMode = SslMode.Require
			};

			services.AddEntityFrameworkNpgsql()
			.AddDbContext<DBContext>(options => {
				options.UseLazyLoadingProxies();
				options.UseNpgsql(builder.ConnectionString);
			});

			/*services.AddDbContext<DBContext>(options =>
			{
				options.UseLazyLoadingProxies();
				options.UseSqlServer(Configuration.GetConnectionString("ConnectionStringDB"));
			}, ServiceLifetime.Transient);*/
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			Bot.Get().Wait();
		}
	}
}

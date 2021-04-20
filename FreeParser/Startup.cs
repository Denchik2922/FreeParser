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

			services.AddDbContext<DBContext>(options =>
			{
				options.UseLazyLoadingProxies();
				options.UseSqlServer(Configuration.GetConnectionString("ConnectionStringDB"));
			}, ServiceLifetime.Transient);
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

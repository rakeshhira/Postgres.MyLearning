using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using pgWebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace pgWebApi
{
	public class Startup
	{
		public static ILoggerFactory MyLoggerFactory = null;

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

			var connection = @"Host=localhost;Database=my_pgConsoleApp;Username=postgres;Password=guest";
			if (MyLoggerFactory == null)
			{
				MyLoggerFactory = services.BuildServiceProvider().GetService<ILoggerFactory>();
			}

			services
				.AddEntityFrameworkNpgsql()
				.AddDbContext<BloggingContext>(dbContextOptions => dbContextOptions
					//.UseLoggerFactory(MyLoggerFactory) 
					.UseNpgsql(connection, connOptions => connOptions.EnableRetryOnFailure()));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseMvc();
		}
	}
}

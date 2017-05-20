using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AOPDemo.Repo;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AOPDemo
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

		public IContainer ApplicationContainer { get; private set; }

		public IConfigurationRoot Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
			// Add framework services.
			services.AddMvc(opt =>
			{
				opt.Filters.Add(typeof(AOPDemo.Filters.LogAspect));
			}).AddControllersAsServices();

			// Create a container.
			var builder = new ContainerBuilder();

			builder.RegisterType<AOPDemo.Aspect.LoggerAspect>();

			builder.RegisterType<AOPDemo.Repo.Repository>()
							   .As<AOPDemo.Repo.IRepository>()
							   .EnableInterfaceInterceptors()
							   .InterceptedBy(typeof(AOPDemo.Aspect.LoggerAspect));
			builder.Populate(services);

			builder.RegisterType<AOPDemo.Controllers.ValuesController>();
			builder.RegisterType<AOPDemo.Controllers.CustomersController>().PropertiesAutowired();

			this.ApplicationContainer = builder.Build();

			return new AutofacServiceProvider(this.ApplicationContainer);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(
			IApplicationBuilder app,
			IHostingEnvironment env,
			ILoggerFactory loggerFactory,
			IApplicationLifetime appLifeTime)
		{
			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddDebug();

			app.UseMvc();

			appLifeTime.ApplicationStopped.Register(() => this.ApplicationContainer.Dispose());
		}
    }
}

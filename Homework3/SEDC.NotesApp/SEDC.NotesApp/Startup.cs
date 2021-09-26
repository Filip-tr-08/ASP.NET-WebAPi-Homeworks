using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SEDC.NotesApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEDC.NotesApp
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
            services.AddControllers();

            //get the value for AppSettings property in appSettings.json
            var appSettingsConfiguration = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsConfiguration);

            AppSettings appSettings = appSettingsConfiguration.Get<AppSettings>();

            DependencyInjectionHelper.InjectDbContext(services, appSettings.DbConnectionString);
            DependencyInjectionHelper.InjectRepositories(services);
            //DependencyInjectionHelper.InjectAdoRepositories(services, appSettings.DbConnectionString);
            DependencyInjectionHelper.InjectDapperRepositories(services, appSettings.DbConnectionString);
            DependencyInjectionHelper.InjectServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

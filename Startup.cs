using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Serilog;
using Serilog.Exceptions;
using System.Reflection;
using System.IO;
using CapitalFunds.Api.Helpers;

namespace CapitalFunds.Api
{
    public class Startup
    {       

        private const string CurrentAppVersion = "v1";
        private string AppName { get; } = Assembly.GetExecutingAssembly().GetName().Name;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<Serilog.ILogger>(x => new LoggerConfiguration()
                .CreateLogger())
                .AddSwaggerGen(s =>
                {
                    s.SwaggerDoc(CurrentAppVersion, new Info { Title = AppName, Version = CurrentAppVersion });
                    s.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{AppName}.xml"));
                });

            services.AddMvc();
            services.AddHttpContextAccessor();



            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app
                .UseHttpsRedirection()
                .UseStaticHttpContext()
                .UseSwagger()
                .UseSwaggerUI(s =>
                {
                    s.RoutePrefix = string.Empty;
                    s.SwaggerEndpoint("./swagger/v1/swagger.json", $"{AppName} {CurrentAppVersion}");
                })
                .UseMvc();
        }
    }
}

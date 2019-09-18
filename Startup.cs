using System;
using System.IO;
using System.Reflection;
using geolocalizationApi.Database;
using geolocalizationApi.Middlewares;
using geolocalizationApi.Repositories;
using geolocalizationApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace geolocalizationApi
{
    public class Startup
    {
        private const string API_VERSION = "v1";
        private const string API_TITLE = "Geolocalization API";

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<GeolocalizationService>();
            services.AddTransient<GeolocalizationRepository>();
            services.AddSingleton(serviceProvider => new DbProvider(Configuration["DbProvider:DatabasePath"]));

            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
            });

            services.AddHttpContextAccessor(); // It will hep us tp get the caller IP address

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(API_VERSION, new OpenApiInfo { Title = API_TITLE, Version = API_VERSION });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{API_TITLE} - {API_VERSION}");
                c.RoutePrefix = string.Empty;
            });
            app.UseMvc();
        }
    }
}

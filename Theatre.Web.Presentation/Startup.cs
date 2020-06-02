using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Theatre.Web.Infrastructure.Data.Context;
using Theatre.Web.Infrastructure.IOC;

namespace Theatre.Web.Presentation
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        private const string CONNECTION_STRING_PATH = "ConnectionStrings:Theatre";
        private const string ALLOWED_HOSTS = "TestEndpoints:Allowed";
        private const string MIGRATION_ASSEMBLY = "Theatre.Web.Presentation";
        private const string REPOSITORIES_NAMESPACE = "Theatre.Web.Infrastructure.Data.Repositories.Implementations";
        private const string SERVICES_NAMESPACE = "Theatre.Web.Core.Services.Implementations";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var time = DateTime.UtcNow;
            var test = JsonConvert.SerializeObject(time);
            services.Configure<KestrelServerOptions>(options => { options.AllowSynchronousIO = true; });
            services.Configure<IISServerOptions>(options => { options.AllowSynchronousIO = true; });
            services.InjectMySqlDbContext<DataContext>(Configuration[CONNECTION_STRING_PATH], MIGRATION_ASSEMBLY);
            services.InjectForNamespace(REPOSITORIES_NAMESPACE);
            services.InjectForNamespace(SERVICES_NAMESPACE); 
            
            services.AddCors(options =>
            {
                options.AddPolicy("_allow", builder =>
                {
                    builder
                        .WithOrigins(Configuration[ALLOWED_HOSTS].Split(","))
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            services.AddControllers();
            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("_allow");

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
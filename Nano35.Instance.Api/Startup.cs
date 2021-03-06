using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nano35.Contracts;
using Nano35.Instance.Api.Configurations;
using Nano35.Instance.Api.Middlewares;

namespace Nano35.Instance.Api
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        
        public Startup()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("ServicesConfig.json");
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            new Configurator(services, new AuthenticationConfiguration(Configuration)).Configure();
            new Configurator(services, new CorsConfiguration()).Configure();
            new Configurator(services, new SwaggerConfiguration()).Configure();
            new Configurator(services, new MassTransitConfiguration()).Configure();
            new Configurator(services, new ConfigurationOfControllers()).Configure();
            new Configurator(services, new ConfigurationOfAuthStateProvider()).Configure();
            services.AddHealthChecks();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            ConfigureCommon.Configure(app);
            ConfigureEndpoints.Configure(app);
        }
    }
}

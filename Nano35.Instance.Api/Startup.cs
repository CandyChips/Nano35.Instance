using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nano35.Instance.Api.Services.AppStart.Configurations;
using Nano35.Instance.Api.Services.AppStart.ConfigureServices;

namespace Nano35.Instance.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            AuthenticationServiceConstructor.Construct(services);
            CorsServiceConstructor.Construct(services);
            MassTransitServiceConstructor.Construct(services);
            MediatRServiceConstructor.Construct(services);
            SwaggerServiceConstructor.Construct(services);
            services.AddControllers();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            ConfigureCommon.Configure(app);
            ConfigureEndpoints.Configure(app);
        }
    }
}

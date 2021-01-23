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
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            AuthenticationServiceConstructor.Construct(services);
            CorsServiceConstructor.Construct(services);
            MassTransitServiceConstructor.Construct(services);
            MediatRServiceConstructor.Construct(services);
            SwaggerServiceConstructor.Construct(services);
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            ConfigureCommon.Configure(app);
            ConfigureEndpoints.Configure(app);
        }
    }
}

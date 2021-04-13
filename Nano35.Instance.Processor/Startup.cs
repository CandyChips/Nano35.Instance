using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nano35.Contracts;
using Nano35.Instance.Processor.Configurations;

namespace Nano35.Instance.Processor
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
            new Configurator(services, new AutoMapperConfiguration()).Configure();
            new Configurator(services, new EntityFrameworkConfiguration(Configuration)).Configure();
            new Configurator(services, new MassTransitConfiguration()).Configure();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) { }
    }
}

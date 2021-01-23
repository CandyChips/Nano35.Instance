using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Nano35.Instance.Processor.Services.AppStart.Configure;

namespace Nano35.Instance.Processor
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            AutoMapperServiceConstructor.Construct(services);
            new EntityFrameworkServiceConstruct("192.168.100.120", "Nano35.Instance.DB", "sa", "Cerber666").Register(services);
            MassTransitServiceConstructor.Construct(services);
            MediatRServiceConstructor.Construct(services);
            
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }
    }
}

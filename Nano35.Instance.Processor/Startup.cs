using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nano35.Instance.Processor.Services.AppStart.Configure;

namespace Nano35.Instance.Processor
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            AutoMapperServiceConstructor.Construct(services);
            EntityFrameworkServiceConstructor.Construct(services);
            MassTransitServiceConstructor.Construct(services);
            
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }
    }
}

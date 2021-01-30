using Microsoft.Extensions.DependencyInjection;
using Nano35.Contracts;

namespace Nano35.Instance.Api.Configurations
{
    public class CorsConfiguration : 
        IConfigurationOfService
    {
        public void AddToServices(
            IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy("Cors", builder => 
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()));
        }
    }
}
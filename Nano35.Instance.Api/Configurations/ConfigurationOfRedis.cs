using Microsoft.Extensions.DependencyInjection;
using Nano35.Contracts;
using StackExchange.Redis;

namespace Nano35.Instance.Api.Configurations
{
    public class RedisConfiguration : 
        IConfigurationOfService
    {
        public void AddToServices(
            IServiceCollection services)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "192.168.100.120:6379";
                options.InstanceName = "master";
            });
        }
    }
}
using Autofac;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Nano35.Instance.Api.Services.Requests.Behaviours;

namespace Nano35.Instance.Api.Services.AppStart.Configurations
{
    public static class MediatRServiceConstructor
    {
        public static void Construct(IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(CacheQueryPipeLineBehaviour<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggerQueryDecorator<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggerCommandDecorator<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipeLineBehaviour<,>));
        }
    }
}
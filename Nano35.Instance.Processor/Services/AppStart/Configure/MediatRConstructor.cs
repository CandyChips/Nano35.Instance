using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Nano35.Instance.Processor.Services.Requests.Behaviours;

namespace Nano35.Instance.Processor.Services.AppStart.Configure
{
    public static class MediatRServiceConstructor
    {
        public static void Construct(IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggerQueryDecorator<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggerCommandDecorator<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionPipeLineBehaviour<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipeLineBehaviour<,>));
        }
    }
}
using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetAllInstanceTypes
{
    public class GetAllInstanceTypesConsumer : IConsumer<IGetAllInstanceTypesRequestContract>
    {
        private readonly IServiceProvider  _services;
        public GetAllInstanceTypesConsumer(IServiceProvider services) => _services = services;
        public async Task Consume(ConsumeContext<IGetAllInstanceTypesRequestContract> context)
        {
            var result = 
                await new LoggedPipeNode<IGetAllInstanceTypesRequestContract, IGetAllInstanceTypesResultContract>(
                    _services.GetService(typeof(ILogger<IGetAllInstanceTypesRequestContract>)) as ILogger<IGetAllInstanceTypesRequestContract>,
                    new GetAllInstanceTypes(_services.GetService(typeof(ApplicationContext)) as ApplicationContext))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}
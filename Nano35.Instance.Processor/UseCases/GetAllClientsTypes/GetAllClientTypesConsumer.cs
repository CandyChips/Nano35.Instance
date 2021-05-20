using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetAllClientsTypes
{
    public class GetAllClientTypesConsumer : IConsumer<IGetAllClientTypesRequestContract>
    {
        private readonly IServiceProvider _services;
        public GetAllClientTypesConsumer(IServiceProvider services) => _services = services;
        public async Task Consume(ConsumeContext<IGetAllClientTypesRequestContract> context)
        {
            var result = 
                await new LoggedPipeNode<IGetAllClientTypesRequestContract, IGetAllClientTypesResultContract>(
                    _services.GetService(typeof(ILogger<IGetAllClientTypesRequestContract>)) as ILogger<IGetAllClientTypesRequestContract>,
                    new GetAllClientTypes(_services.GetService(typeof(ApplicationContext)) as ApplicationContext))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}
using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetAllUnitTypes
{
    public class GetAllUnitTypesConsumer : IConsumer<IGetAllUnitTypesRequestContract>
    {
        private readonly IServiceProvider  _services;
        public GetAllUnitTypesConsumer(IServiceProvider services) => _services = services;
        public async Task Consume(ConsumeContext<IGetAllUnitTypesRequestContract> context)
        {
            var result = 
                await new LoggedPipeNode<IGetAllUnitTypesRequestContract, IGetAllUnitTypesResultContract>(
                    _services.GetService(typeof(ILogger<IGetAllUnitTypesRequestContract>)) as ILogger<IGetAllUnitTypesRequestContract>,
                    new GetAllUnitTypes(_services.GetService(typeof(ApplicationContext)) as ApplicationContext))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}
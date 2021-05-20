using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetAllUnits
{
    public class GetAllUnitsConsumer : IConsumer<IGetAllUnitsRequestContract>
    {
        private readonly IServiceProvider  _services;
        public GetAllUnitsConsumer(IServiceProvider services) => _services = services;
        public async Task Consume(ConsumeContext<IGetAllUnitsRequestContract> context)
        {
            var result = 
                await new LoggedPipeNode<IGetAllUnitsRequestContract, IGetAllUnitsResultContract>(
                    _services.GetService(typeof(ILogger<IGetAllUnitsRequestContract>)) as ILogger<IGetAllUnitsRequestContract>,
                    new GetAllUnits(_services.GetService(typeof(ApplicationContext)) as ApplicationContext, _services.GetService(typeof(IBus)) as IBus))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}
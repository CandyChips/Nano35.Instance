using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetUnitStringsByIds
{
    public class GetUnitStringsByIdsConsumer : IConsumer<IGetUnitStringsByIdsRequestContract>
    {
        private readonly IServiceProvider  _services;
        public GetUnitStringsByIdsConsumer(IServiceProvider services) => _services = services;
        public async Task Consume(ConsumeContext<IGetUnitStringsByIdsRequestContract> context)
        {
            var result =
                await new LoggedPipeNode<IGetUnitStringsByIdsRequestContract, IGetUnitStringsByIdsResultContract>(
                    _services.GetService(typeof(ILogger<IGetUnitStringsByIdsRequestContract>)) as ILogger<IGetUnitStringsByIdsRequestContract>,
                        new GetUnitStringsByIds(_services.GetService(typeof(ApplicationContext)) as ApplicationContext))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}
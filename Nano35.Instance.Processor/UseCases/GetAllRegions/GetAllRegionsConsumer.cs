using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetAllRegions
{
    public class GetAllRegionsConsumer : IConsumer<IGetAllRegionsRequestContract>
    {
        private readonly IServiceProvider  _services;
        public GetAllRegionsConsumer(IServiceProvider services) => _services = services;
        public async Task Consume(ConsumeContext<IGetAllRegionsRequestContract> context)
        {
            var result =
                await new LoggedUseCasePipeNode<IGetAllRegionsRequestContract, IGetAllRegionsSuccessResultContract>(
                    _services.GetService(typeof(ILogger<IGetAllRegionsRequestContract>)) as ILogger<IGetAllRegionsRequestContract>,
                    new GetAllRegionsUseCase(_services.GetService(typeof(ApplicationContext)) as ApplicationContext))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}
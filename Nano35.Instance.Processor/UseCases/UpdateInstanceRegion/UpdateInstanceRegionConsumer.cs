using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateInstanceRegion
{
    public class UpdateInstanceRegionConsumer : IConsumer<IUpdateInstanceRegionRequestContract>
    {
        private readonly IServiceProvider  _services;
        public UpdateInstanceRegionConsumer(IServiceProvider services) => _services = services;
        public async Task Consume(ConsumeContext<IUpdateInstanceRegionRequestContract> context)
        {
            var dbContext = (ApplicationContext)_services.GetService(typeof(ApplicationContext));
            var result = 
                await new LoggedUseCasePipeNode<IUpdateInstanceRegionRequestContract, IUpdateInstanceRegionResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateInstanceRegionRequestContract>)) as ILogger<IUpdateInstanceRegionRequestContract>,
                    new TransactedUseCasePipeNode<IUpdateInstanceRegionRequestContract, IUpdateInstanceRegionResultContract>(dbContext,
                        new UpdateInstanceRegionUseCase(dbContext)))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}
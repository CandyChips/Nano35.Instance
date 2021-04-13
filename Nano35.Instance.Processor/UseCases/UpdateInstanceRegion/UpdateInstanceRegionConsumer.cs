using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateInstanceRegion
{
    public class UpdateInstanceRegionConsumer : 
        IConsumer<IUpdateInstanceRegionRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public UpdateInstanceRegionConsumer(IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(ConsumeContext<IUpdateInstanceRegionRequestContract> context)
        {
            var dbContext = (ApplicationContext)_services.GetService(typeof(ApplicationContext));
            var result = await new LoggedPipeNode<IUpdateInstanceRegionRequestContract, IUpdateInstanceRegionResultContract>(
                _services.GetService(typeof(ILogger<IUpdateInstanceRegionRequestContract>)) as ILogger<IUpdateInstanceRegionRequestContract>,
                    new TransactedPipeNode<IUpdateInstanceRegionRequestContract, IUpdateInstanceRegionResultContract>(dbContext,
                        new UpdateInstanceRegionUseCase(dbContext)))
                .Ask(context.Message, context.CancellationToken);
            switch (result)
            {
                case IUpdateInstanceRegionSuccessResultContract:
                    await context.RespondAsync<IUpdateInstanceRegionSuccessResultContract>(result);
                    break;
                case IUpdateInstanceRegionErrorResultContract:
                    await context.RespondAsync<IUpdateInstanceRegionErrorResultContract>(result);
                    break;
            }
        }
    }
}
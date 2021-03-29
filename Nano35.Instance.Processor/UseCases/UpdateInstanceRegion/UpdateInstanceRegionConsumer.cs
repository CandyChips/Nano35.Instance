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
        
        public UpdateInstanceRegionConsumer(
            IServiceProvider services)
        {
            _services = services;
        }


        public async Task Consume(
            ConsumeContext<IUpdateInstanceRegionRequestContract> context)
        {
            var dbcontect = (ApplicationContext)_services.GetService(typeof(ApplicationContext)); 
            var logger = (ILogger<LoggedUpdateInstanceRegionRequest>) _services.GetService(typeof(ILogger<LoggedUpdateInstanceRegionRequest>));
            
            var message = context.Message;
            
            var result =
                await new LoggedUpdateInstanceRegionRequest(logger,
                    new ValidatedUpdateInstanceRegionRequest(
                        new TransactedUpdateInstanceRegionRequest(dbcontect,
                            new UpdateInstanceRegionUseCase(dbcontect)))).Ask(message, context.CancellationToken);
            
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
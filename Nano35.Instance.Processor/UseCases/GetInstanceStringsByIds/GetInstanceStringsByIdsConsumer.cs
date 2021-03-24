using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetInstanceStringsByIds
{
    public class GetInstanceStringsByIdsConsumer : 
        IConsumer<IGetInstanceStringsByIdsRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public GetInstanceStringsByIdsConsumer(
            IServiceProvider services)
        {
            _services = services;
        }

        public async Task Consume(
            ConsumeContext<IGetInstanceStringsByIdsRequestContract> context)
        {
            var dbContext = (ApplicationContext)_services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<LoggedGetInstanceStringsByIdsRequest>) _services.GetService(typeof(ILogger<LoggedGetInstanceStringsByIdsRequest>));
            
            var message = context.Message;
            
            var result =
                await new LoggedGetInstanceStringsByIdsRequest(logger,
                    new ValidatedGetInstanceStringsByIdsRequest(
                        new GetInstanceStringsByIdsRequest(dbContext))
                ).Ask(message, context.CancellationToken);
            
            switch (result)
            {
                case IGetInstanceStringsByIdsSuccessResultContract:
                    await context.RespondAsync<IGetInstanceStringsByIdsSuccessResultContract>(result);
                    break;
                case IGetInstanceStringsByIdsErrorResultContract:
                    await context.RespondAsync<IGetInstanceStringsByIdsErrorResultContract>(result);
                    break;
            }
        }
    }
}
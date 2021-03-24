using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetUnitStringsByIds
{
    public class GetUnitStringsByIdsConsumer : 
        IConsumer<IGetUnitStringsByIdsRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public GetUnitStringsByIdsConsumer(
            IServiceProvider services)
        {
            _services = services;
        }

        public async Task Consume(
            ConsumeContext<IGetUnitStringsByIdsRequestContract> context)
        {
            var dbContext = (ApplicationContext)_services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<LoggedGetUnitStringsByIdsRequest>) _services.GetService(typeof(ILogger<LoggedGetUnitStringsByIdsRequest>));
            
            var message = context.Message;
            
            var result =
                await new LoggedGetUnitStringsByIdsRequest(logger,
                    new ValidatedGetUnitStringsByIdsRequest(
                        new GetUnitStringsByIdsRequest(dbContext))
                ).Ask(message, context.CancellationToken);
            
            switch (result)
            {
                case IGetUnitStringsByIdsSuccessResultContract:
                    await context.RespondAsync<IGetUnitStringsByIdsSuccessResultContract>(result);
                    break;
                case IGetUnitStringsByIdsErrorResultContract:
                    await context.RespondAsync<IGetUnitStringsByIdsErrorResultContract>(result);
                    break;
            }
        }
    }
}
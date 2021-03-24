using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.UseCases.GetClientStringsByIds;

namespace Nano35.Instance.Processor.UseCases.GetClientStringsByIds
{
    public class GetClientStringsByIdsConsumer : 
        IConsumer<IGetClientStringsByIdsRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public GetClientStringsByIdsConsumer(
            IServiceProvider services)
        {
            _services = services;
        }

        public async Task Consume(
            ConsumeContext<IGetClientStringsByIdsRequestContract> context)
        {
            var dbContext = (ApplicationContext)_services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<LoggedGetClientStringsByIdsRequest>) _services.GetService(typeof(ILogger<LoggedGetClientStringsByIdsRequest>));
            
            var message = context.Message;
            
            var result =
                await new LoggedGetClientStringsByIdsRequest(logger,
                    new ValidatedGetClientStringsByIdRequest(
                        new GetClientStringsByIdsRequest(dbContext))
                ).Ask(message, context.CancellationToken);
            
            switch (result)
            {
                case IGetClientStringsByIdsSuccessResultContract:
                    await context.RespondAsync<IGetClientStringsByIdsSuccessResultContract>(result);
                    break;
                case IGetClientStringsByIdsErrorResultContract:
                    await context.RespondAsync<IGetClientStringsByIdsErrorResultContract>(result);
                    break;
            }
        }
    }
}
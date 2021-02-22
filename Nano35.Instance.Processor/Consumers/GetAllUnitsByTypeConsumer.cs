using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Requests.GetAllUnitsByType;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.Consumers
{
    public class GetAllUnitsByTypeConsumer : 
        IConsumer<IGetAllUnitsByTypeRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public GetAllUnitsByTypeConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllUnitsByTypeRequestContract> context)
        {
            var dbcontect = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<LoggedGetAllUnitsByTypeRequest>) _services.GetService(typeof(ILogger<LoggedGetAllUnitsByTypeRequest>));
            
            var message = context.Message;
            
            var result =
                await new LoggedGetAllUnitsByTypeRequest(logger,
                    new ValidatedGetAllUnitsByTypeRequest(
                        new GetAllUnitsByTypeRequest(dbcontect))
                ).Ask(message, context.CancellationToken);
            
            switch (result)
            {
                case IGetAllUnitsByTypeSuccessResultContract:
                    await context.RespondAsync<IGetAllUnitsByTypeSuccessResultContract>(result);
                    break;
                case IGetAllUnitsByTypeErrorResultContract:
                    await context.RespondAsync<IGetAllUnitsByTypeErrorResultContract>(result);
                    break;
            }
        }
    }
}
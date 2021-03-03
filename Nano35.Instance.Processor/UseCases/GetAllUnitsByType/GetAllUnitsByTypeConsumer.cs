using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetAllUnitsByType
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
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<LoggedGetAllUnitsByTypeRequest>) _services.GetService(typeof(ILogger<LoggedGetAllUnitsByTypeRequest>));
            
            var message = context.Message;
            
            var result =
                await new LoggedGetAllUnitsByTypeRequest(logger,
                    new ValidatedGetAllUnitsByTypeRequest(
                        new GetAllUnitsByTypeRequest(dbContext))
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
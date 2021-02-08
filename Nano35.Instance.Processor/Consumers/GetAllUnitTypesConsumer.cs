using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Requests;
using Nano35.Instance.Processor.Requests.GetAllUnitTypes;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.Consumers
{
    public class GetAllUnitTypesConsumer : 
        IConsumer<IGetAllUnitTypesRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public GetAllUnitTypesConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllUnitTypesRequestContract> context)
        {
            var dbcontect = (ApplicationContext)_services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<GetAllUnitTypesLogger>) _services.GetService(typeof(ILogger<GetAllUnitTypesLogger>));
            
            var message = context.Message;
            
            var result =
                await new GetAllUnitTypesLogger(logger,
                    new GetAllUnitTypesValidator(
                        new GetAllUnitTypesRequest(dbcontect))
                ).Ask(message, context.CancellationToken);
            
            switch (result)
            {
                case IGetAllUnitTypesSuccessResultContract:
                    await context.RespondAsync<IGetAllUnitTypesSuccessResultContract>(result);
                    break;
                case IGetAllUnitTypesErrorResultContract:
                    await context.RespondAsync<IGetAllUnitTypesErrorResultContract>(result);
                    break;
            }
        }
    }
}
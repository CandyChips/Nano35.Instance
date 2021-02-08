using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Requests;
using Nano35.Instance.Processor.Requests.GetAllUnits;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.Consumers
{
    public class GetAllUnitsConsumer : 
        IConsumer<IGetAllUnitsRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public GetAllUnitsConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllUnitsRequestContract> context)
        {
            var dbcontect = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<GetAllUnitsLogger>) _services.GetService(typeof(ILogger<GetAllUnitsLogger>));
            
            var message = context.Message;
            
            var result =
                await new GetAllUnitsLogger(logger,
                    new GetAllUnitsValidator(
                        new GetAllUnitsRequest(dbcontect))
                ).Ask(message, context.CancellationToken);
            
            switch (result)
            {
                case IGetAllUnitsSuccessResultContract:
                    await context.RespondAsync<IGetAllUnitsSuccessResultContract>(result);
                    break;
                case IGetAllUnitsErrorResultContract:
                    await context.RespondAsync<IGetAllUnitsErrorResultContract>(result);
                    break;
            }
        }
    }
}
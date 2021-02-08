using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Requests.GetAllClientsStates;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.Consumers
{
    public class GetAllClientStatesConsumer : 
        IConsumer<IGetAllClientStatesRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public GetAllClientStatesConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllClientStatesRequestContract> context)
        {
            var dbcontect = (ApplicationContext)_services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<GetAllClientStatesLogger>) _services.GetService(typeof(ILogger<GetAllClientStatesLogger>));
            
            var message = context.Message;
            
            var result =
                await new GetAllClientStatesLogger(logger,
                    new GetAllClientStatesValidator(
                        new GetAllClientStatesRequest(dbcontect))
                ).Ask(message, context.CancellationToken);
            
            switch (result)
            {
                case IGetAllClientStatesSuccessResultContract:
                    await context.RespondAsync<IGetAllClientStatesSuccessResultContract>(result);
                    break;
                case IGetAllClientStatesErrorResultContract:
                    await context.RespondAsync<IGetAllClientStatesErrorResultContract>(result);
                    break;
            }
        }
    }
}
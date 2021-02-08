using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Requests.GetAllClients;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.Consumers
{
    public class GetAllClientsConsumer : 
        IConsumer<IGetAllClientsRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public GetAllClientsConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllClientsRequestContract> context)
        {
            var dbcontect = (ApplicationContext)_services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<GetAllClientsLogger>) _services.GetService(typeof(ILogger<GetAllClientsLogger>));
            
            var message = context.Message;
            
            var result =
                await new GetAllClientsLogger(logger,
                    new GetAllClientsValidator(
                        new GetAllClientsRequest(dbcontect))
                ).Ask(message, context.CancellationToken);
            
            switch (result)
            {
                case IGetAllClientsSuccessResultContract:
                    await context.RespondAsync<IGetAllClientsSuccessResultContract>(result);
                    break;
                case IGetAllClientsErrorResultContract:
                    await context.RespondAsync<IGetAllClientsErrorResultContract>(result);
                    break;
            }
        }
    }
}
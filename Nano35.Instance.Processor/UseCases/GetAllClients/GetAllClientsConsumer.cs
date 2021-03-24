using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetAllClients
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
            var dbContext = (ApplicationContext)_services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<LoggedGetAllClientsRequest>) _services.GetService(typeof(ILogger<LoggedGetAllClientsRequest>));
            
            var message = context.Message;
            
            var result =
                await new LoggedGetAllClientsRequest(logger,
                    new ValidatedGetAllClientsRequest(
                        new GetAllClientsRequest(dbContext))).Ask(message, context.CancellationToken);
            
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
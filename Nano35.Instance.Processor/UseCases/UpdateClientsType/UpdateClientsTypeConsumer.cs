using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateClientsType
{
    public class UpdateClientsTypeConsumer : 
        IConsumer<IUpdateClientsTypeRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public UpdateClientsTypeConsumer(
            IServiceProvider services)
        {
            _services = services;
        }


        public async Task Consume(
            ConsumeContext<IUpdateClientsTypeRequestContract> context)
        {
            var dbcontect = (ApplicationContext)_services.GetService(typeof(ApplicationContext)); 
            var logger = (ILogger<LoggedUpdateClientsTypeRequest>) _services.GetService(typeof(ILogger<LoggedUpdateClientsTypeRequest>));
            
            var message = context.Message;
            
            var result =
                await new LoggedUpdateClientsTypeRequest(logger,
                    new ValidatedUpdateClientsTypeRequest(
                        new TransactedUpdateClientsTypeRequest(dbcontect,
                            new UpdateClientsTypeRequest(dbcontect)))).Ask(message, context.CancellationToken);
            
            switch (result)
            {
                case IUpdateClientsTypeSuccessResultContract:
                    await context.RespondAsync<IUpdateClientsTypeSuccessResultContract>(result);
                    break;
                case IUpdateClientsTypeErrorResultContract:
                    await context.RespondAsync<IUpdateClientsTypeErrorResultContract>(result);
                    break;
            }
        }
    }
}
using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateClientsState
{
    public class UpdateClientsStateConsumer : 
        IConsumer<IUpdateClientsStateRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public UpdateClientsStateConsumer(
            IServiceProvider services)
        {
            _services = services;
        }


        public async Task Consume(
            ConsumeContext<IUpdateClientsStateRequestContract> context)
        {
            var dbcontect = (ApplicationContext)_services.GetService(typeof(ApplicationContext)); 
            var logger = (ILogger<LoggedUpdateClientsStateRequest>) _services.GetService(typeof(ILogger<LoggedUpdateClientsStateRequest>));
            
            var message = context.Message;
            
            var result =
                await new LoggedUpdateClientsStateRequest(logger,
                    new ValidatedUpdateClientsStateRequest(
                        new TransactedUpdateClientsStateRequest(dbcontect,
                            new UpdateClientsStateRequest(dbcontect)))).Ask(message, context.CancellationToken);
            
            switch (result)
            {
                case IUpdateClientsStateSuccessResultContract:
                    await context.RespondAsync<IUpdateClientsStateSuccessResultContract>(result);
                    break;
                case IUpdateClientsStateErrorResultContract:
                    await context.RespondAsync<IUpdateClientsStateErrorResultContract>(result);
                    break;
            }
        }
    }
}
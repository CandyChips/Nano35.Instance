using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateClientsName
{
    public class UpdateClientsNameConsumer : 
        IConsumer<IUpdateClientsNameRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public UpdateClientsNameConsumer(
            IServiceProvider services)
        {
            _services = services;
        }


        public async Task Consume(
            ConsumeContext<IUpdateClientsNameRequestContract> context)
        {
            var dbcontect = (ApplicationContext)_services.GetService(typeof(ApplicationContext)); 
            var logger = (ILogger<LoggedUpdateClientsNameRequest>) _services.GetService(typeof(ILogger<LoggedUpdateClientsNameRequest>));
            
            var message = context.Message;
            
            var result =
                await new LoggedUpdateClientsNameRequest(logger,
                    new ValidatedUpdateClientsNameRequest(
                        new TransactedUpdateClientsNameRequest(dbcontect,
                            new UpdateClientsNameRequest(dbcontect)))).Ask(message, context.CancellationToken);
            
            switch (result)
            {
                case IUpdateClientsNameSuccessResultContract:
                    await context.RespondAsync<IUpdateClientsNameSuccessResultContract>(result);
                    break;
                case IUpdateClientsNameErrorResultContract:
                    await context.RespondAsync<IUpdateClientsNameErrorResultContract>(result);
                    break;
            }
        }
    }
}
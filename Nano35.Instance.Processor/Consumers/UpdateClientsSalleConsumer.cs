using System;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Requests;
using Nano35.Instance.Processor.Requests.UpdateClientsSelle;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.Consumers
{
    public class UpdateClientsSelleConsumer : 
        IConsumer<IUpdateClientsSelleRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public UpdateClientsSelleConsumer(
            IServiceProvider services)
        {
            _services = services;
        }


        public async Task Consume(
            ConsumeContext<IUpdateClientsSelleRequestContract> context)
        {
            var dbcontect = (ApplicationContext)_services.GetService(typeof(ApplicationContext)); 
            var logger = (ILogger<LoggedUpdateClientsSelleRequest>) _services.GetService(typeof(ILogger<LoggedUpdateClientsSelleRequest>));
            
            var message = context.Message;
            
            var result =
                await new LoggedUpdateClientsSelleRequest(logger,
                    new ValidatedUpdateClientsSelleRequest(
                        new TransactedUpdateClientsSelleRequest(dbcontect,
                            new UpdateClientsSelleRequest(dbcontect)))
                ).Ask(message, context.CancellationToken);
            
            switch (result)
            {
                case IUpdateClientsSelleSuccessResultContract:
                    await context.RespondAsync<IUpdateClientsSelleSuccessResultContract>(result);
                    break;
                case IUpdateClientsSelleErrorResultContract:
                    await context.RespondAsync<IUpdateClientsSelleErrorResultContract>(result);
                    break;
            }
        }
    }
}
using System;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Requests;
using Nano35.Instance.Processor.Requests.UpdateClientsSalle;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.Consumers
{
    public class UpdateClientsSalleConsumer : 
        IConsumer<IUpdateClientsSalleRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public UpdateClientsSalleConsumer(
            IServiceProvider services)
        {
            _services = services;
        }


        public async Task Consume(
            ConsumeContext<IUpdateClientsSalleRequestContract> context)
        {
            var dbcontect = (ApplicationContext)_services.GetService(typeof(ApplicationContext)); 
            var logger = (ILogger<LoggedUpdateClientsSalleRequest>) _services.GetService(typeof(ILogger<LoggedUpdateClientsSalleRequest>));
            
            var message = context.Message;
            
            var result =
                await new LoggedUpdateClientsSalleRequest(logger,
                    new ValidatedUpdateClientsSalleRequest(
                        new TransactedUpdateClientsSalleRequest(dbcontect,
                            new UpdateClientsSalleRequest(dbcontect)))
                ).Ask(message, context.CancellationToken);
            
            switch (result)
            {
                case IUpdateClientsSalleSuccessResultContract:
                    await context.RespondAsync<IUpdateClientsSalleSuccessResultContract>(result);
                    break;
                case IUpdateClientsSalleErrorResultContract:
                    await context.RespondAsync<IUpdateClientsSalleErrorResultContract>(result);
                    break;
            }
        }
    }
}
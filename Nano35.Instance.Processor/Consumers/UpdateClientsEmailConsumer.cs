using System;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Requests;
using Nano35.Instance.Processor.Requests.UpdateClientsEmail;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.Consumers
{
    public class UpdateClientsEmailConsumer : 
        IConsumer<IUpdateClientsEmailRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public UpdateClientsEmailConsumer(
            IServiceProvider services)
        {
            _services = services;
        }


        public async Task Consume(
            ConsumeContext<IUpdateClientsEmailRequestContract> context)
        {
            var dbcontect = (ApplicationContext)_services.GetService(typeof(ApplicationContext)); 
            var logger = (ILogger<LoggedUpdateClientsEmailRequest>) _services.GetService(typeof(ILogger<LoggedUpdateClientsEmailRequest>));
            
            var message = context.Message;
            
            var result =
                await new LoggedUpdateClientsEmailRequest(logger,
                    new ValidatedUpdateClientsEmailRequest(
                        new TransactedUpdateClientsEmailRequest(dbcontect,
                            new UpdateClientsEmailRequest(dbcontect)))
                ).Ask(message, context.CancellationToken);
            
            switch (result)
            {
                case IUpdateClientsEmailSuccessResultContract:
                    await context.RespondAsync<IUpdateClientsEmailSuccessResultContract>(result);
                    break;
                case IUpdateClientsEmailErrorResultContract:
                    await context.RespondAsync<IUpdateClientsEmailErrorResultContract>(result);
                    break;
            }
        }
    }
}
using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateClientsPhone
{
    public class UpdateClientsPhoneConsumer : 
        IConsumer<IUpdateClientsPhoneRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public UpdateClientsPhoneConsumer(
            IServiceProvider services)
        {
            _services = services;
        }


        public async Task Consume(
            ConsumeContext<IUpdateClientsPhoneRequestContract> context)
        {
            var dbcontect = (ApplicationContext)_services.GetService(typeof(ApplicationContext)); 
            var logger = (ILogger<LoggedUpdateClientsPhoneRequest>) _services.GetService(typeof(ILogger<LoggedUpdateClientsPhoneRequest>));
            
            var message = context.Message;
            
            var result =
                await new LoggedUpdateClientsPhoneRequest(logger,
                    new ValidatedUpdateClientsPhoneRequest(
                        new TransactedUpdateClientsPhoneRequest(dbcontect,
                            new UpdateClientsPhoneRequest(dbcontect)))).Ask(message, context.CancellationToken);
            
            switch (result)
            {
                case IUpdateClientsPhoneSuccessResultContract:
                    await context.RespondAsync<IUpdateClientsPhoneSuccessResultContract>(result);
                    break;
                case IUpdateClientsPhoneErrorResultContract:
                    await context.RespondAsync<IUpdateClientsPhoneErrorResultContract>(result);
                    break;
            }
        }
    }
}
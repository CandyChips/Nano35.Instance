using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateUnitsAdress
{
    public class UpdateUnitsAddressConsumer : 
        IConsumer<IUpdateUnitsAddressRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public UpdateUnitsAddressConsumer(
            IServiceProvider services)
        {
            _services = services;
        }


        public async Task Consume(
            ConsumeContext<IUpdateUnitsAddressRequestContract> context)
        {
            var dbContext = (ApplicationContext)_services.GetService(typeof(ApplicationContext)); 
            var logger = (ILogger<LoggedUpdateUnitsAddressRequest>) _services.GetService(typeof(ILogger<LoggedUpdateUnitsAddressRequest>));
            
            var message = context.Message;
            
            var result =
                await new LoggedUpdateUnitsAddressRequest(logger,
                    new ValidatedUpdateUnitsAddressRequest(
                        new TransactedUpdateUnitsAddressRequest(dbContext,
                            new UpdateUnitsAddressRequest(dbContext)))
                ).Ask(message, context.CancellationToken);
            
            switch (result)
            {
                case IUpdateUnitsAddressSuccessResultContract:
                    await context.RespondAsync<IUpdateUnitsAddressSuccessResultContract>(result);
                    break;
                case IUpdateUnitsAddressErrorResultContract:
                    await context.RespondAsync<IUpdateUnitsAddressErrorResultContract>(result);
                    break;
            }
        }
    }
}
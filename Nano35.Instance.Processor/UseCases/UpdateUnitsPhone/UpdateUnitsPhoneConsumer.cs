using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateUnitsPhone
{
    public class UpdateUnitsPhoneConsumer : 
        IConsumer<IUpdateUnitsPhoneRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public UpdateUnitsPhoneConsumer(
            IServiceProvider services)
        {
            _services = services;
        }


        public async Task Consume(
            ConsumeContext<IUpdateUnitsPhoneRequestContract> context)
        {
            var dbcontect = (ApplicationContext)_services.GetService(typeof(ApplicationContext)); 
            var logger = (ILogger<LoggedUpdateUnitsPhoneRequest>) _services.GetService(typeof(ILogger<LoggedUpdateUnitsPhoneRequest>));
            
            var message = context.Message;
            
            var result =
                await new LoggedUpdateUnitsPhoneRequest(logger,
                    new ValidatedUpdateUnitsPhoneRequest(
                        new TransactedUpdateUnitsPhoneRequest(dbcontect,
                            new UpdateUnitsPhoneRequest(dbcontect)))
                ).Ask(message, context.CancellationToken);
            
            switch (result)
            {
                case IUpdateUnitsPhoneSuccessResultContract:
                    await context.RespondAsync<IUpdateUnitsPhoneSuccessResultContract>(result);
                    break;
                case IUpdateUnitsPhoneErrorResultContract:
                    await context.RespondAsync<IUpdateUnitsPhoneErrorResultContract>(result);
                    break;
            }
        }
    }
}
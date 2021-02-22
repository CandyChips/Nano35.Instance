using System;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Requests;
using Nano35.Instance.Processor.Requests.UpdateUnitsAdress;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.Consumers
{
    public class UpdateUnitsAdressConsumer : 
        IConsumer<IUpdateUnitsAdressRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public UpdateUnitsAdressConsumer(
            IServiceProvider services)
        {
            _services = services;
        }


        public async Task Consume(
            ConsumeContext<IUpdateUnitsAdressRequestContract> context)
        {
            var dbcontect = (ApplicationContext)_services.GetService(typeof(ApplicationContext)); 
            var logger = (ILogger<LoggedUpdateUnitsAdressRequest>) _services.GetService(typeof(ILogger<LoggedUpdateUnitsAdressRequest>));
            
            var message = context.Message;
            
            var result =
                await new LoggedUpdateUnitsAdressRequest(logger,
                    new ValidatedUpdateUnitsAdressRequest(
                        new TransactedUpdateUnitsAdressRequest(dbcontect,
                            new UpdateUnitsAdressRequest(dbcontect)))
                ).Ask(message, context.CancellationToken);
            
            switch (result)
            {
                case IUpdateUnitsAdressSuccessResultContract:
                    await context.RespondAsync<IUpdateUnitsAdressSuccessResultContract>(result);
                    break;
                case IUpdateUnitsAdressErrorResultContract:
                    await context.RespondAsync<IUpdateUnitsAdressErrorResultContract>(result);
                    break;
            }
        }
    }
}
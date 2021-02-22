using System;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Requests;
using Nano35.Instance.Processor.Requests.UpdateUnitsType;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.Consumers
{
    public class UpdateUnitsTypeConsumer : 
        IConsumer<IUpdateUnitsTypeRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public UpdateUnitsTypeConsumer(
            IServiceProvider services)
        {
            _services = services;
        }


        public async Task Consume(
            ConsumeContext<IUpdateUnitsTypeRequestContract> context)
        {
            var dbcontect = (ApplicationContext)_services.GetService(typeof(ApplicationContext)); 
            var logger = (ILogger<LoggedUpdateUnitsTypeRequest>) _services.GetService(typeof(ILogger<LoggedUpdateUnitsTypeRequest>));
            
            var message = context.Message;
            
            var result =
                await new LoggedUpdateUnitsTypeRequest(logger,
                    new ValidatedUpdateUnitsTypeRequest(
                        new TransactedUpdateUnitsTypeRequest(dbcontect,
                            new UpdateUnitsTypeRequest(dbcontect)))
                ).Ask(message, context.CancellationToken);
            
            switch (result)
            {
                case IUpdateUnitsTypeSuccessResultContract:
                    await context.RespondAsync<IUpdateUnitsTypeSuccessResultContract>(result);
                    break;
                case IUpdateUnitsTypeErrorResultContract:
                    await context.RespondAsync<IUpdateUnitsTypeErrorResultContract>(result);
                    break;
            }
        }
    }
}